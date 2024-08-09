using Preqin.Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Preqin.Application.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<PaginatedList<T>> GetAllAsync(int pageIndex, int pageSize);
        Task<T?> GetByIdAsync(int id);
    }
}
