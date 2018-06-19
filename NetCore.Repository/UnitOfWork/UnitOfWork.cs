using NetCore.Domain.Entities;
using NetCore.Repository.RepositoryBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Repository.UnitOfWork
{
   public class UnitOfWork : IUnitOfWork
    {
        private IDatabaseFactory _databaseFactory;
        private EfDbContext dataContext;

        public UnitOfWork(IDatabaseFactory databaseFactory)
        {
            this._databaseFactory = databaseFactory;
        }

        protected EfDbContext DataContext
        {
            get { return dataContext ?? (dataContext = _databaseFactory.GetEfDbContext()); }
        }

        public void Commit()
        {
            DataContext.SaveChanges();
        }

        public void CommitAsync()
        {
            DataContext.SaveChangesAsync();
        }
    }
}
