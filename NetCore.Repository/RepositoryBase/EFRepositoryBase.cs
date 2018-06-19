using Microsoft.EntityFrameworkCore;
using NetCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NetCore.Domain.Models.EntitiesModels;

namespace NetCore.Repository.RepositoryBase
{
    public abstract class EFRepositoryBase<T> where T : class
    {
        private EfDbContext dataContext;
        private readonly DbSet<T> dbSet;

        protected IDatabaseFactory DatabaseFactory
        {
            private set;
            get;
        }

        public EfDbContext DataContext {
            get { return dataContext ?? (dataContext = DatabaseFactory.GetEfDbContext()); }
        }

        public EFRepositoryBase(IDatabaseFactory databaseFactory)
        {
            DatabaseFactory = databaseFactory;
            dbSet = DataContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        //新增方法
        public virtual void AddAll(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        //新增方法
        public virtual void Update(IEnumerable<T> entities)
        {
            foreach (T obj in entities)
            {
                dbSet.Attach(obj);
                dataContext.Entry(obj).State = EntityState.Modified;
            }
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void Delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            dbSet.RemoveRange(objects);
        }

        //新增方法
        public virtual void DeleteAll(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual void Clear()
        {
            throw new NotImplementedException();
        }

        public virtual T GetById(long id)
        {
            return dbSet.Find(id);
        }

        public virtual T GetById(string id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        public T Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual IEnumerable<T> GetAllLazy()
        {
            return dbSet;
        }
    }
}
