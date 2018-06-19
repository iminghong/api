using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Api.Commons.AppConfigs
{
    public class MongodbConnectionSetting
    {
        public string Mongodbservice { get; set; }

        public string MongodbPort { get; set; }

        public string MongodbName { get; set; }
    }
}
