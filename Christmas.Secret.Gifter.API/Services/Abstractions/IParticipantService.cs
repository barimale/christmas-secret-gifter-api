using Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions.Scoped;
using Christmas.Secret.Gifter.Domain;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Services.Abstractions
{
    public interface IParticipantService : IBaseRepositoryOuterScope<Participant, string>, IBaseRepositoryInnerScope<Participant, string>
    {
        Task<Participant[]> GetAllAsync(string eventId, CancellationToken? cancellationToken = null);
    }
}
