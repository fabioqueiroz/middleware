using Microsoft.EntityFrameworkCore;
using Middleware.Data.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Middleware.Api
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal Context<TEntity> _context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(Context<TEntity> context)
        {
            this._context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> GetWithRawSql(string query,
            params object[] parameters)
        {
            return null;
            //return dbSet.SqlQuery(query, parameters).ToList();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public virtual void Add(TEntity entity)
        {

            try
            {
                dbSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
            }
        }

        public virtual void DeleteById(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (_context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            _context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            try
            {
                _context.Add<TEntity>(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                _ = ex.ToString();
                return null;
            }
        }
    }
}
