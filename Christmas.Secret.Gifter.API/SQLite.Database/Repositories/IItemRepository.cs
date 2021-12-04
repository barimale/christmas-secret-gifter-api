using Albergue.Administrator.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Albergue.Administrator.SQLite.Database.Repositories
{
    public interface IItemRepository
    {
        Task<ShopItem> AddAsync(ShopItem item, CancellationToken cancellationToken);
        Task<int> DeleteAsync(string id, CancellationToken cancellationToken);
        Task<ShopItem[]> GetAllAsync(CancellationToken? cancellationToken = null);
        Task<ShopItem[]> GetByCategoryIdAsync(string categoryId, CancellationToken cancellationToken);
        Task<ShopItem> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<ShopItem> UpdateAsync(ShopItem item, CancellationToken cancellationToken);
    }
}