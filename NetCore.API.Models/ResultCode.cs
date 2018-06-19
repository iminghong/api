using System;
using System.ComponentModel;

namespace NetCore.API.Models
{
    public enum ResultCode
    {
        /// <summary>
        /// 默认情况
        /// </summary>
        [Description("")]
        Default=0,

        /// <summary>
        /// 成功
        /// </summary>
        [Description("成功")]
        Access=1,

        /// <summary>
        /// 失败
        /// </summary>
        [Description("失败")]
        Fail = -1,

        /// <summary>
        /// 错误
        /// </summary>
        [Description("错误")]
        Error = 1000,
    }
}
