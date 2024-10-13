using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    public interface IGenericRepository<T> where T : class
    {
        IAsyncEnumerable<T> GetAllAsync();
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(long id);
        Task<Boolean> Add(T entity);
        Task<Boolean> Delete(long id);
        Task<Boolean> Update(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, Boolean>> predicate);
    }
}
