using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindCustomerService.DataAccess
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties,
            int take, int skip);

        IEnumerable<TEntity> Get( Expression<Func<TEntity, bool>> filter,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy,
            string includeProperties,
            int take, int skip);

        int GetCount(); //for paging
        Task<int> GetCountAsync(); 

        TEntity Find(object id);
        Task<TEntity> FindAsync(object id);
        void Add(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Delete(object id);
        void Delete(TEntity entityToDelete);
    }
}
