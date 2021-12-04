using Albergue.Administrator.Model;
using System.Threading;
using System.Threading.Tasks;

namespace Albergue.Administrator.SQLite.Database.Repositories
{
    public interface ILanguageRepository
    {
        Task<Language> AddAsync(Language item, CancellationToken? cancellationToken = null);
        Task<int> DeleteAsync(string id, CancellationToken? cancellationToken = null);
        Task<Language[]> GetAllAsync(CancellationToken? cancellationToken = null);
    }
}