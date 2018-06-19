using MongoDB.Driver;
using NetCore.Domain.Models.MongodbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Domain.Mongodb
{
    public interface IMongodbContext
    {
        /// <summary>
        /// Gets a <see cref="MongodbSetting"/> instance which contains the settings
        /// information used by current context.
        /// </summary>
        MongodbSetting Settings { get; }

        /// <summary>
        /// Gets the <see cref="MongoCollection"/> instance by the given <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> object.</param>
        /// <returns>The <see cref="MongoCollection"/> instance.</returns>
        IMongoCollection<T> GetCollection<T>() where T : IMongodbEntity;
    }
}
