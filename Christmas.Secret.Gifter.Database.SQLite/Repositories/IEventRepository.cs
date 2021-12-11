using Christmas.Secret.Gifter.Domain;

namespace Christmas.Secret.Gifter.Database.SQLite.SQLite.Database.Repositories
{
    public interface IEventRepository
    {
        Task<Event> AddAsync(Event item, CancellationToken cancellationToken);
        Task<int> DeleteAsync(string id, CancellationToken cancellationToken);
        Task<Event[]> GetAllAsync(CancellationToken? cancellationToken = null);
        Task<Event> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<Event> UpdateAsync(Event item, CancellationToken cancellationToken);
    }
}