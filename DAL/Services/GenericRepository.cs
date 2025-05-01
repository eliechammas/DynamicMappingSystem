using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DataContext context;
        protected DbSet<T> dbSet;
        protected readonly ILogger _logger;

        public GenericRepository(DataContext context, ILogger logger)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
            this._logger = logger;
        }

        #region CRUD
        public virtual async IAsyncEnumerable<T> GetAllAsync() 
        {
            var query = dbSet.AsAsyncEnumerable();
            await foreach (var record in query)
            {
                yield return record;
            }
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            var query = await dbSet.ToListAsync();
            return query;
        }

        public virtual async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null
                                                        , Expression<Func<T, bool>> order = null 
                                                        , Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null
                                                        , string includeProperties = ""
                                                        )
        {
            IQueryable<T> query = dbSet;

            /// filter is a predicate that is sent to the repository and based on it the dbSet is filtered
            if (filter != null)
            {
                query = query.Where(filter);
            }

            /// to include the related entities
            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            /// order is a predicate that is sent to the repository and based on it the query is ordered
            if(order != null)
            {
                return await query.OrderBy(order).ToListAsync();
            }
            else
            {
                if (order == null && orderBy != null)
                {
                    return await orderBy(query).ToListAsync();
                }
                else
                {
                    return await query.ToListAsync();
                }
            }
        }

        public virtual async Task<T> GetById(long id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<Boolean> Add(T entity)
        {
            await dbSet.AddAsync(entity);
            return true;
        }

        public virtual async Task<Boolean> Update(T entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            return true;
        }

        public virtual async Task<Boolean> Delete(long id)
        {
            T entityToDelete = await dbSet.FindAsync(id);
            dbSet.Remove(entityToDelete);
            return true;
        }

        public virtual async Task<IEnumerable<T>> Find(Expression<Func<T, Boolean>> predicate)
        {
            return await dbSet.Where(predicate).ToListAsync();
        }
        #endregion

    }
}
