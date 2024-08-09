using Microsoft.EntityFrameworkCore;
using Preqin.Application.Models;
using Preqin.Application.Repositories;
using Preqin.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Preqin.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly PreqinDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(PreqinDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<PaginatedList<T>> GetAllAsync(int pageIndex, int pageSize)
        {
            var count = await _dbSet.CountAsync();
            var items = await _dbSet.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    }
}
