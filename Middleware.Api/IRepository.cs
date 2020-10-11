using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entityToDelete);
        void DeleteById(object id);
        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetByID(object id);
        IEnumerable<TEntity> GetWithRawSql(string query, params object[] parameters);
        void Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
