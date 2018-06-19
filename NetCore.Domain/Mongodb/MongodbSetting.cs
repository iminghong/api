using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Domain.Mongodb
{
    public class MongodbSetting
    {
        public MongodbSetting(string mongodbservice, string mongodbPort, string mongodbName)
        {
            Mongodbservice = mongodbservice;
            MongodbPort = int.Parse(mongodbPort);
            MongodbName = mongodbName;
        }

        public string Mongodbservice { get; set; }
        public int MongodbPort { get; set; }
        public string MongodbName { get; set; }

        /// <summary>
        /// 获取MongoDB的服务器配置信息。
        /// </summary>
        public MongoClientSettings ClientSettings
        {
            get
            {
                var settings = new MongoClientSettings();
                settings.Server = new MongoServerAddress(Mongodbservice, MongodbPort);
                settings.WriteConcern = WriteConcern.Acknowledged;
                return settings;
            }
        }
        /// <summary>
        /// 获取数据库配置信息。
        /// </summary>
        /// <param name="server">需要配置的数据库实例。</param>
        /// <returns>数据库配置信息。</returns>
        public MongoDatabaseSettings GetDatabaseSettings(MongoClient client)
        {
            // 您无需做过多的更改：此处仅返回新建的MongoDatabaseSettings实例即可。
            return new MongoDatabaseSettings();
        }
    }
}
