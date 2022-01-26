using Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions.Scoped;
using Christmas.Secret.Gifter.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Services.Abstractions
{
    public interface IEventService : IBaseRepositoryOuterScope<Event, string>
    {
        Task<Event> ExecuteAsync(string eventId, CancellationToken cancellationToken = default);
    }
}
