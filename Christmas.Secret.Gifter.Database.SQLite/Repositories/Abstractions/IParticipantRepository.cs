using Christmas.Secret.Gifter.Database.SQLite.Entries;

namespace Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions
{
    public interface IParticipantRepository : IBaseRepository<ParticipantEntry, string>
    {
        Task<ParticipantEntry[]> GetAllAsync(string eventId, CancellationToken? cancellationToken = null);
    }
}