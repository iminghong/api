using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore.API.Filter
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            //NLog.Logger log = NLog.LogManager.GetCurrentClassLogger();
            //if (context.Implementation.GetType().Namespace.Contains("NetCore"))
            //{
            //    log = NLog.LogManager.GetLogger(context.Implementation.GetType().FullName);
            //}

            //var logModel = new ApiLog();
            //try
            //{
            //    //log.Error("Before");

            //    logModel.Id = IdCreator.Default.Create();
            //    logModel.InputTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    logModel.Namespace = context.Implementation.GetType().Namespace;
            //    logModel.ClassName = context.Implementation.GetType().Name;
            //    logModel.MethodName = context.ImplementationMethod.Name;
            //    logModel.LogType = "api";
            //    IsoDateTimeConverter timeformat = new IsoDateTimeConverter();
            //    timeformat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";

            //    try
            //    {
            //        JsonSerializerSettings settings = new JsonSerializerSettings
            //        {
            //            NullValueHandling = NullValueHandling.Ignore,
            //            MissingMemberHandling = MissingMemberHandling.Error,
            //            Error = delegate (object obj, Newtonsoft.Json.Serialization.ErrorEventArgs args)
            //            {
            //                args.ErrorContext.Handled = false;
            //            }
            //        };
            //        settings.Converters.Add(new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.RoundtripKind });
            //        logModel.Parameter = JsonConvert.SerializeObject(context.Parameters, Formatting.Indented, settings);
            //    }
            //    catch (Exception ex)
            //    {
            //        var dictionary = new Dictionary<string, string>();
            //        context.Parameters.ToList().ForEach(p =>
            //        {
            //            try
            //            {
            //                var json = JsonConvert.SerializeObject(p, Formatting.Indented, timeformat);
            //                dictionary.Add(p.GetType().Name, json);
            //            }
            //            catch (Exception exx)
            //            {
            //                dictionary.Add(p.GetType().Name, p.ToString());
            //            }
            //        });

            //        logModel.Parameter = JsonConvert.SerializeObject(dictionary, Formatting.Indented, timeformat);
            //    }

            //    Stopwatch sw = new Stopwatch();
            //    sw.Start();
            //    //耗时巨大的代码 
            //    await next(context);
            //    sw.Stop();
            //    TimeSpan tspan = sw.Elapsed;
            //    logModel.ResultValue = JsonConvert.SerializeObject(context.ReturnValue, Formatting.Indented, timeformat);
            //    logModel.ExecTime = tspan.Milliseconds.ToString();
            //}
            //catch (Exception ex)
            //{
            //    logModel.Exception = ex.ToString();
            //    //log.Debug("ex");
            //    throw;
            //}
            //finally
            //{
            //    var json = JsonConvert.SerializeObject(logModel, Formatting.Indented);
            //    log.Error(json);
            //}


            base.OnException(context);
        }

        public override Task OnExceptionAsync(ExceptionContext context)
        {
            return base.OnExceptionAsync(context);
        }
    }
}
