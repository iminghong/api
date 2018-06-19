using NetCore.Api.Commons;
using System;

namespace NetCore.Api.IService
{
    public interface ICFDAppInfoService
    {
        [AopInterceptor]
        string Get();
    }
}
