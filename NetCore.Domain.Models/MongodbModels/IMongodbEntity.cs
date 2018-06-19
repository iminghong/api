using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Domain.Models.MongodbModels
{
    public interface IMongodbEntity
    {
       [BsonId]
       long Id { get; set; }
    }
}
