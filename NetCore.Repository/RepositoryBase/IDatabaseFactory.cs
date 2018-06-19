using NetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Repository.RepositoryBase
{
    public interface IDatabaseFactory : IDisposable
    {
        EfDbContext GetEfDbContext();
    }
}
