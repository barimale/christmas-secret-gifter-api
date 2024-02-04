using Christmas.Secret.Gifter.API.Queries;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Christmas.Secret.Gifter.Domain;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Handlers
{
    internal sealed class ParticipantQueriesHandler :
                IRequestHandler<GetParticipantByIdQuery, Participant>
    {
        private readonly IParticipantService _participantService;
        public ParticipantQueriesHandler(IParticipantService participantService)
            => _participantService = participantService;

        public Task<Participant> Handle(GetParticipantByIdQuery request, CancellationToken cancellationToken)
        {
            return _participantService.GetByIdAsync(request.ParticipantId, cancellationToken);
        }
    }
}
