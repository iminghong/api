using MongoDB.Driver;
using NetCore.Domain.Models.MongodbModels;
using NetCore.Domain.Mongodb;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace NetCore.Repository.Mongodb
{
    public class MongodbRepositoryBase<T> where T : class, IMongodbEntity
    {
        #region Private Fields
        private readonly IMongodbContext mongodbContext;
        private IMongoCollection<T> collection;

        public MongodbRepositoryBase(IMongodbContext _mongodbContext)
        {
            this.mongodbContext = _mongodbContext;
            collection = mongodbContext.GetCollection<T>();
        }
        #endregion

        /// <summary>
        /// MongoBD Context，一个集合
        /// </summary>
        protected IMongoCollection<T> DataContext
        {
            get { return collection ?? (collection = mongodbContext.GetCollection<T>()); }
        }

        /// <summary>
        /// 添加类型实例到Mongo
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            collection.InsertOneAsync(entity).Wait();
        }
 
        /// <summary>
        /// 添加集合到Mongo
        /// </summary>
        /// <param name="entities"></param>
        public void AddAll(IEnumerable<T> entities)
        {
            collection.InsertManyAsync(entities).Wait();
        }
 
        /// <summary>
        /// 更新实例
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            var filter = Builders<T>.Filter.Where(t => t.Id == entity.Id);
            collection.ReplaceOneAsync(filter, entity).Wait();
        }
 
        /// <summary>
        /// 更新一组实例
        /// </summary>
        /// <param name="entities"></param>
        public void Update(IEnumerable<T> entities)
        {
            foreach (var it in entities)
            {
                Update(it);
            }
        }
 
        /// <summary>
        /// 更新或添加一组实例
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrAddAll(IEnumerable<T> entities)
        {
            var list2 = new List<T>(); //will be add
            foreach (var i in entities)
            {
                if (GetById(i.Id) != null)
                {
                    Update(i);
                }
                else
                {
                    list2.Add(i);
                }
            }
            if (list2.Count > 0)
            {
                AddAll(list2);
            }
        }

        /// <summary>
        /// 删除实例
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            var filter = Builders<T>.Filter.Where(t => t.Id == entity.Id);
            collection.DeleteOneAsync(filter).Wait();
        }

        /// <summary>
        /// 按条件删除
        /// </summary>
        /// <param name="where"></param>
        public void Delete(Expression<Func<T, bool>> where)
        {
            collection.DeleteOneAsync(where).Wait();
        }

        /// <summary>
        /// 删除一组实例
        /// </summary>
        /// <param name="entities"></param>
        public void DeleteAll(IEnumerable<T> entities)
        {
            var filter = Builders<T>.Filter.Where(t => t.Id > 0);
            collection.DeleteManyAsync(filter).Wait();
        }

        /// <summary>
        /// 按Id 范围删除
        /// </summary>
        /// <param name="MinId"></param>
        /// <param name="MaxId"></param>
        public void DeleteMany(int MinId, int MaxId)
        {
            var filter = Builders<T>.Filter.Where(t => t.Id >= MinId && t.Id <= MaxId);
            collection.DeleteManyAsync(filter).Wait();
        }

        /// <summary>
        /// 清除所以
        /// </summary>
        public void Clear()
        {
            var filter = Builders<T>.Filter.Where(t => t.Id > 0);
            collection.DeleteManyAsync(filter).Wait();
        }

        /// <summary>
        /// 按id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T GetById(long Id)
        {
            return collection.Find(t => t.Id == Id).FirstOrDefaultAsync().Result;
        }

        /// <summary>
        /// 按id查询
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public T GetById(string Id)
        {
            var entityId = 0;
            int.TryParse(Id, out entityId);
            return collection.Find(t => t.Id == entityId).FirstOrDefaultAsync().Result;
        }

        /// <summary>
        /// 按条件查询
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> where)
        {
            return collection.Find(where).FirstOrDefaultAsync().Result;
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return collection.Find(t => t.Id > 0).ToListAsync().Result;
        }

        /// <summary>
        /// 按条件获取
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public IEnumerable<T> GetMany(Expression<Func<T, bool>> where)
        {
            return collection.Find(where).ToListAsync().Result;
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAllLazy()
        {
            return collection.Find(t => t.Id > 0).ToListAsync().Result;
        }
    }
}
