using NetCore.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.API.Models.ResultModel
{
    public class ApiResultBase
    {
        public ApiResultBase(ResultCode code= ResultCode.Default, string message="", dynamic result=null,string responseTime ="")
        {
            Code = code;
            Message = string.IsNullOrEmpty(message)?code.GetDescription(): message;
            Result = result ?? "";
            ResponseTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public virtual ResultCode Code { get; set; }

        public virtual string Message { get; set; }

        public virtual dynamic Result { get; set; }

        public virtual string ResponseTime { get; set; }
    }
}
