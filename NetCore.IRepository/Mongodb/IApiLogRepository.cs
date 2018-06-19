using NetCore.Domain.Models.MongodbModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.IRepository.Mongodb
{
    public interface IApiLogRepository: IRepository<ApiLog>
    {
    }
}
