using Albergue.Administrator.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Albergue.Administrator.SQLite.Database.Repositories
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category item, CancellationToken cancellationToken);
        Task<int> DeleteAsync(string id, CancellationToken cancellationToken);
        Task<Category[]> GetAllAsync(CancellationToken? cancellationToken = null);
        Task<Category> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<Category> UpdateAsync(Category item, CancellationToken cancellationToken);
    }
}