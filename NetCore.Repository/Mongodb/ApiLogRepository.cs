using NetCore.Domain.Models.MongodbModels;
using NetCore.Domain.Mongodb;
using NetCore.IRepository.Mongodb;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Repository.Mongodb
{
    public class ApiLogRepository : MongodbRepositoryBase<ApiLog>, IApiLogRepository
    {
        public ApiLogRepository(IMongodbContext _mongodbContext) : base(_mongodbContext)
        {
        }
    }
}
