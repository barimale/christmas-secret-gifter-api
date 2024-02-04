using Christmas.Secret.Gifter.API.Queries;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Algorithm.ConstraintsPairing.Model.Responses;
using Christmas.Secret.Gifter.Domain;
using Algorithm.ConstraintsPairing;
using Algorithm.ConstraintsPairing.Model;

namespace Christmas.Secret.Gifter.API.Handlers
{
    internal sealed class ParticipantCommandsHandler :
        IRequestHandler<AddParticipantCommand, Participant>
    {
        private readonly IParticipantService _participantService;

        public ParticipantCommandsHandler(IParticipantService participantService)
            => _participantService = participantService;

        public Task<Participant> Handle(AddParticipantCommand request, CancellationToken cancellationToken)
        {
            return _participantService.AddAsync(request.Participant, cancellationToken);
        }
    }
}
