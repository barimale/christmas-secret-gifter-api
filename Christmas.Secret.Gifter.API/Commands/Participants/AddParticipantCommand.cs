using Christmas.Secret.Gifter.Domain;
using MediatR;

namespace Christmas.Secret.Gifter.API.Commands.Participants
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
