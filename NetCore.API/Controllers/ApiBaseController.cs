using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCore.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ApiBaseController : Controller
    {
        public virtual string AppId { get; set; }

        public virtual string Channel { get; set; }

        public virtual string UserToken { get; set; }

        public virtual string Version { get; set; }

        public virtual string RequestDate { get; set; }

        public virtual string Sign { get; set; }

        public ApiBaseController()
        {
        }
    }
}