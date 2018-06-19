using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Api.IService;
using Newtonsoft.Json;

namespace NetCore.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class CFDAppInfoController : ApiBaseController
    {
        private readonly ICFDAppInfoService _iCFDAppInfoService;

        public CFDAppInfoController(ICFDAppInfoService iCFDAppInfoService)
        {
            this._iCFDAppInfoService = iCFDAppInfoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var model= _iCFDAppInfoService.Get();
            return Content(model);
        }
    }
}