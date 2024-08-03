using Christmas.Secret.Gifter.Application.Queries;
using Christmas.Secret.Gifter.Application.Services.Abstractions;
using Christmas.Secret.Gifter.Domain;
using MediatR;

namespace Christmas.Secret.Gifter.Application.Handlers.ParticipantHandlers
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
