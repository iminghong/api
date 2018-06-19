using NetCore.Domain.Models.EntitiesModels;
using NetCore.IRepository;
using NetCore.Repository.RepositoryBase;
using System;

namespace NetCore.Repository
{
    public class CFDAppInfoRepository : EFRepositoryBase<CFDAppInfo>, ICFDAppInfoRepository
    {
        public CFDAppInfoRepository(IDatabaseFactory databaseFactory) 
            : base(databaseFactory)
        {
        }
    }
}
