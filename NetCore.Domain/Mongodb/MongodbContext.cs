using MongoDB.Driver;
using NetCore.Domain.Models.MongodbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Domain.Mongodb
{
    public class MongodbContext: IMongodbContext
    {
        private readonly MongodbSetting settings;
        private readonly MongoClient client;
        private readonly IMongoDatabase db;

        /// <summary>
        /// 构造，初始化MongoClient
        /// </summary>
        /// <param name="settings"></param>
        public MongodbContext(MongodbSetting settings)
        {
            this.settings = settings;
            client = new MongoClient(settings.ClientSettings);
            db = client.GetDatabase(settings.MongodbName);
        }

        /// <summary>
        /// Mongo 设置
        /// </summary>
        public MongodbSetting Settings
        {
            get
            {
                return settings;
            }
        }

        /// <summary>
        /// 获取一个集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IMongoCollection<T> GetCollection<T>() where T : IMongodbEntity
        {
            return db.GetCollection<T>(typeof(T).Name);
        }

    }
}
