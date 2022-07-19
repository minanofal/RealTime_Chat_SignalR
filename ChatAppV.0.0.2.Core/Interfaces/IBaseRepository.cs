using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> GetAllAsyncNoTraking(Expression<Func<T,bool>> match= null , string[] includes = null);
        Task<T> GetAsyncTraking(Expression<Func<T, bool>> match = null, string[] includes = null);
        Task<bool> DeleteAsync(T entity);
        Task<bool> DeleteAsync(Expression<Func<T, bool>> match);
        Task<bool> DeleteRangeAsync(IEnumerable<T> entities);
        Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> match);
        Task<List<T>> GetRangeForEdite(Expression<Func<T, bool>> match);
    }
}
