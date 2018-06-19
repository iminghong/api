using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Api.IService;
using NetCore.API.Authorization;

namespace NetCore.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class ActivityController : ApiBaseController
    {
        private readonly IActivityService _iActivityService;
        public ActivityController(IActivityService iActivityService)
        {
            this._iActivityService = iActivityService;
        }

        [HttpGet]
        [ApiAuthorize("Permission")]
        public IActionResult GetAll()
        {
            var result = _iActivityService.Get();
            //throw (new Exception("111111"));
            return Content(result);
        }
    }
}