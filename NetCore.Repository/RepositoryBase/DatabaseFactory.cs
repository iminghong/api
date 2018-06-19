using System;
using System.Collections.Generic;
using System.Text;
using NetCore.Domain.Entities;

namespace NetCore.Repository.RepositoryBase
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private EfDbContext dataContext;

        public DatabaseFactory(EfDbContext efDbContext)
        {
            this.dataContext = efDbContext;
        }

        public EfDbContext GetEfDbContext()
        {
            return dataContext; //?? (dataContext = new EfDbContext());
        }

        public override void DisposeCore()
        {
            if (dataContext != null)
                dataContext.Dispose();
        }
    }
}
