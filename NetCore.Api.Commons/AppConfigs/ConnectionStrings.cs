using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Api.Commons.AppConfigs
{
    public class ConnectionStrings
    {
        public string DefaultConnection { get; set; }

        public MongodbConnectionSetting MongodbConnection { get; set; }

    }
}
