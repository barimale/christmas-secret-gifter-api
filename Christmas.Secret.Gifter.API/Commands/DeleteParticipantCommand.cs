using MediatR;

namespace Christmas.Secret.Gifter.API.Queries
{
    public record DeleteParticipantCommand : IRequest<int>
    {
        public DeleteParticipantCommand(string participantId)
        {
            ParticipantId = participantId;
        }

        public string ParticipantId { get; private set; }
    }
}
