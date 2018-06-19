using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using NetCore.Api.Commons;
using NetCore.Api.Commons.AppConfigs;
using NetCore.API.Authorization;
using NetCore.API.Filter;
using NetCore.Domain.Entities;
using NetCore.Domain.Mongodb;
using NetCore.IRepository;
using NetCore.Repository;
using NetCore.Repository.RepositoryBase;
using NetCore.Repository.UnitOfWork;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace NetCore.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            /*
             * EF依赖注入
             * ps:使用AddDbContextPool会出现 connection is open 等占用情况
             */
            //services.AddDbContextPool<EfDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));  //会出现 connection is open 等占用情况
            services.AddDbContext<EfDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //services.AddDbContext<EfDbContext>();

            // Add framework services.
            //依赖注入 若出现重复注册的问题，请使用AddTransient
            services.AddTransient<IDatabaseFactory, DatabaseFactory>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            /* ioc Assembly */
            //获取配置文件转换为对象
            var iocArray = Configuration.GetSection("AppSettings").Get<AppSettings>()?.IocAppSettings;
            AddAssemblyIoc(services, iocArray);

            /* 配置文件注入方式 */
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddOptions();

            /*mongodb 注入*/
            services.AddTransient<MongodbSetting>(p => {
                var mongodbModel = Configuration.GetSection("ConnectionStrings").Get<ConnectionStrings>()?.MongodbConnection;
                return new MongodbSetting(mongodbModel?.Mongodbservice, mongodbModel?.MongodbPort, mongodbModel?.MongodbName);
            });
            services.AddTransient<IMongodbContext,MongodbContext>();

            services.AddMvc(options=> {
                options.Filters.Add(new ApiExceptionFilterAttribute());
                options.Filters.Add(new ApiActionFilterAttribute());
                options.Filters.Add(new ApiAuthorizationFilter());
                options.RespectBrowserAcceptHeader = true;
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            //identityServer4
            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:6000";
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "netcore.api";
                });

            /*https://www.cnblogs.com/daxnet/p/6181366.html*/
            //Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Netcore.Api接口文档",
                    Description = "RESTful API for Netcore.Api",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Alvin_Su", Email = "939391793@qq.com", Url = "" }
                });

                //Set the comments path for the swagger json and ui.
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "NetCore.Api.xml");
                c.IncludeXmlComments(xmlPath);

                //identityServer4
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = "http://localhost:6000",
                    Scopes = new Dictionary<string, string>
                    {
                        { "pwd.client", "secret" }
                    }
                });

                c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });

            //aspect
            services.AddDynamicProxy(config =>
            {
                config.Interceptors.AddTyped<AopInterceptorAttribute>(Predicates.ForService("*Service"));
            });
            return services.BuildAspectCoreServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //注入
            app.UseStaticFiles();

            //nlog
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            loggerFactory.AddNLog();
            loggerFactory.ConfigureNLog("Configs/nlog.config");
            //var log= NLog.LogManager.LoadConfiguration("Configs/nlog.config");

            //IdentityServer4
            app.UseAuthentication();
            app.Use((context, next) =>
            {
                var user = context.User;

                context.Response.StatusCode = user.Identity.IsAuthenticated ? 200 : 401;

                return next.Invoke();
            });

            //Swagger
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NetCore.Api API V1");
                //c.ShowRequestHeaders();
            });

            /*
             DI资料：https://www.cnblogs.com/xishuai/p/asp-net-core-ioc-di-get-service.html
             */
            ApplicationServicesLocator.Instance = app.ApplicationServices;

            app.UseMvc(config =>
            {
                config.MapRoute(
                    name: "Default",
                    template: "api/{controller}/{action}/{id?}",
                    defaults: new { controller = "CFDAppInfo", action = "Get" }
                    );
            });
        }

        private static Dictionary<Type, Type[]> GetClassName(string assemblyName)
        {
            if (!String.IsNullOrEmpty(assemblyName))
            {
                Assembly assembly = Assembly.Load(assemblyName);
                List<Type> ts = assembly.GetTypes().ToList();

                var result = new Dictionary<Type, Type[]>();
                foreach (var item in ts.Where(s => !s.IsInterface))
                {
                    var interfaceType = item.GetInterfaces();
                    if (item.IsGenericType) continue;
                    result.Add(item, interfaceType);
                }
                return result;
            }
            return new Dictionary<Type, Type[]>();
        }

        public static void AddAssemblyIoc(IServiceCollection services, IocSettings[] iocSettings)
        {
            if(iocSettings == null)
            {
                return;
            }
            for(int i=0;i< iocSettings.Length; i++)
            {
                if (iocSettings[i] == null)
                {
                    continue;
                }
                var classDict = GetClassName(iocSettings[i].ClassAssemblyName);
                if (classDict != null)
                {
                    foreach (var item in classDict)
                    {
                        if (item.Key.Name.ToLower().EndsWith(iocSettings[i].ClassNameFormat?.ToLower()))
                        {
                            foreach (var typeArray in item.Value)
                            {
                                var _namespace = typeArray.FullName.ToLower();
                                if (_namespace.Contains(iocSettings[i].InterfaceAssemblyName?.ToLower()))
                                {
                                    services.AddTransient(typeArray, item.Key);
                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
