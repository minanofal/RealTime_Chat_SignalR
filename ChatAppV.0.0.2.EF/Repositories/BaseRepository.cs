using ChatAppV._0._0._2.Core.Interfaces;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<T> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
             _context.Remove(entity);
            var Result = await _context.SaveChangesAsync();
            if (Result != 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteAsync(Expression<Func<T, bool>> match)
        {
            var entity = await GetAsyncTraking(match);
            var result = await DeleteAsync(entity);
            return result;
        }

        public async Task<bool> DeleteRangeAsync(IEnumerable<T> entities)
        {
            _context.RemoveRange(entities);
            var Result = await _context.SaveChangesAsync();
            if (Result != 0)
                return true;
            else
                return false;
        }

        public async Task<bool> DeleteRangeAsync(Expression<Func<T, bool>> match)
        {
            var entities =  await GetAllAsyncNoTraking(match);
            var result = await DeleteRangeAsync(entities);
            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsyncNoTraking(Expression<Func<T, bool>> match = null, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            if (match != null && includes == null)
            {
                query = query.Where(match);
            }
            else if (match == null && includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            else if (match != null && includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
                query = query.Where(match);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetAsyncTraking(Expression<Func<T, bool>> match = null, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (match !=null && includes == null)
            {
                query = query.Where(match);
            }
            else if (match == null && includes != null)
            {
                foreach (var include in includes)
                    query = query.Include(include);
            }
            else if(match != null && includes != null)
            {
                foreach(var include in includes)
                    query = query.Include(include);
                query = query.Where(match);
            }
            return await query.AsQueryable().FirstOrDefaultAsync();

        }

        public async Task<List<T>> GetRangeForEdite(Expression<Func<T, bool>> match)
        {
          
            var l=    await _context.Set<T>().Where(match).ToListAsync();
            return l;
        }
    }
}
