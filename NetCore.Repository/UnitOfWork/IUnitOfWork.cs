using System;
using System.Collections.Generic;
using System.Text;

namespace NetCore.Repository.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Commit();
        void CommitAsync();
    }
}
