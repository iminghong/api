using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.API.Models.InputModel
{
    public class ApiRequestBase
    {
        /// <summary>
        /// 来源
        /// </summary>
        public virtual string AppSource { get; set; }

        /// <summary>
        /// token令牌
        /// </summary>
        public virtual string Token { get; set; }

        /// <summary>
        /// IP
        /// </summary>
        public virtual string Ip { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public virtual string RequestTime { get; set; }
    }
}
