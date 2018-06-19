using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NetCore.Domain.Models.EntitiesModels
{
    [Table("CFD_AppInfo")]
    public class CFDAppInfo
    {
        [Key]
        public virtual int Appid { get; set; }
        public virtual int Proxyid { get; set; }
        public virtual string appkey { get; set; }
        public virtual string appsecret { get; set; }
        public virtual DateTime timeout { get; set; }
        public virtual DateTime updatetime { get; set; }
    }
}
