using System;

namespace NetCore.Api.Commons
{
    public class ApplicationServicesLocator
    {
        /// <summary>
        /// 只能获取AddTransient和AddSingleton注入的对象，而不能获取AddScoped（对象不共享）
        /// </summary>
        public static IServiceProvider Instance { get; set; }
    }
}
