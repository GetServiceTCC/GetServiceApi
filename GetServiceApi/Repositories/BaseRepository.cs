using GetServiceApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace GetServiceApi.Repositories
{
    public class BaseRepository<TEntity> : IDisposable where TEntity : class
    {
        protected ApiDbContext Context { get; private set; }
        protected DbSet<TEntity> Entity { get; set; }

        public BaseRepository()
        {
            Context = new ApiDbContext();
            Entity = Context.Set<TEntity>();
        }
        
        public virtual void Add(TEntity entity)
        {
            Entity.Add(entity);
            Context.SaveChanges();
        }

        public virtual TEntity GetById(int id)
        {
            return Entity.Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Entity.ToList();
        }

        public virtual void Update(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public virtual void Remove(TEntity entity)
        {
            Entity.Remove(entity);
            Context.SaveChanges();
        }
        
        public void Dispose()
        {
            Context.Dispose();
        }
    }
}