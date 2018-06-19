using NetCore.Domain.Models.EntitiesModels;
using NetCore.IRepository;
using NetCore.IRepository.Entities;
using NetCore.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Text;

namespace NetCore.Repository.Entities
{
    public class ActivityRepository : EFRepositoryBase<Domain.Models.EntitiesModels.Activity>, IActivityRepository
    {
        public ActivityRepository(IDatabaseFactory databaseFactory) 
            : base(databaseFactory)
        {

        }

    }
}
