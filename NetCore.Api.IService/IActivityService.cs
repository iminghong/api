using NetCore.Api.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Api.IService
{
    public interface IActivityService
    {
        [AopInterceptor]
        string Get();
    }
}
