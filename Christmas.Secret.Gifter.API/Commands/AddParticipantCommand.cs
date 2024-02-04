using Christmas.Secret.Gifter.Domain;
using MediatR;

namespace Christmas.Secret.Gifter.API.Queries
{
    public record AddParticipantCommand : IRequest<Participant>
    {
        public AddParticipantCommand(Participant participant)
        {
            Participant = participant;
        }

        public Participant Participant { get; private set; }
    }
}
