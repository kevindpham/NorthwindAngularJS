using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NorthwindCustomerService.Model;

namespace NorthwindCustomerService.DataAccess
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class 
    {
        internal NorthwindEntities Ctx;
        internal DbSet<TEntity> DbSet;

        public GenericRepository(NorthwindEntities ctx)
        {
            this.Ctx = ctx;
            this.DbSet = ctx.Set<TEntity>();
        }

        public Task<IEnumerable<TEntity>> GetAsync(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "",int take = 0, int skip = 0)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
               .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
                query = orderBy(query);

            query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            return Task.FromResult(query.AsEnumerable());
        }

        public IEnumerable<TEntity> Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "",int take = 0, int skip = 0)
        {
            IQueryable<TEntity> query = DbSet;

            if (filter != null)
                query = query.Where(filter);

            query = includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
               .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            if (orderBy != null)
                query = orderBy(query);

            query = query.Skip(skip);

            if (take > 0)
                query = query.Take(take);

            return query.AsEnumerable();
        }

        public TEntity Find(object id)
        {
            return DbSet.Find(id);
        }

        public async Task<TEntity> FindAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Ctx.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            TEntity entityToDelete = DbSet.Find(id);
            Delete(entityToDelete);
        }

        public void Delete(TEntity entityToDelete)
        {
            if (Ctx.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }


        public async Task<int> GetCountAsync()
        {
            return await DbSet.CountAsync();
        }


        public int GetCount()
        {
            return DbSet.Count();
        }
    }
}
