using Algorithm.ConstraintsPairing;
using AutoMapper;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Christmas.Secret.Gifter.Database.SQLite.Entries;
using Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions;
using Christmas.Secret.Gifter.Domain;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Services
{
    public class ParticipantService : IParticipantService
    {
        private readonly ILogger<ParticipantService> _logger;
        private readonly Engine _engine;
        private readonly IParticipantRepository _participantRepoistory;
        private readonly IMapper _mapper;

        public ParticipantService(
            ILogger<ParticipantService> logger,
            IParticipantRepository participantRepoistory,
            IMapper mapper)
        {
            _engine = new Engine();
            _logger = logger;
            _participantRepoistory = participantRepoistory;
            _mapper = mapper;
        }

        public async Task<Participant> AddAsync(Participant item, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<ParticipantEntry>(item);
            var added = await _participantRepoistory.AddAsync(mapped, cancellationToken);

            return _mapper.Map<Participant>(added);
        }

        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            var found = await _participantRepoistory.GetByIdAsync(id, cancellationToken);
            var toReorder = found.Event.Participants.Where(p => p.Id != id);

            var deleted = await _participantRepoistory.DeleteAsync(id, cancellationToken);

            int i = 1;
            foreach (var existed in toReorder)
            {
                var modified = existed.ExcludedOrderIds.Where(p => p != existed.OrderId).ToArray();
                existed.ExcludedOrderIds = modified.Append(i).ToArray();
                existed.OrderId = i;
                await _participantRepoistory.UpdateAsync(existed, cancellationToken);
                i = i + 1;
            }

            return deleted;
        }

        public async Task<Participant[]> GetAllAsync(string eventId, CancellationToken? cancellationToken = null)
        {
            var found = await _participantRepoistory.GetAllAsync(eventId, cancellationToken);

            return found.
                Select(p => _mapper.Map<Participant>(p))
                .ToArray();
        }

        public async Task<Participant> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var found = await _participantRepoistory.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<Participant>(found);
        }

        public async Task<Participant> UpdateAsync(Participant item, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<ParticipantEntry>(item);
            var updated = await _participantRepoistory.UpdateAsync(mapped, cancellationToken);

            return _mapper.Map<Participant>(updated);
        }
    }
}
