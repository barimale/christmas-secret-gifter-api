using Christmas.Secret.Gifter.Domain;
using MediatR;

namespace Christmas.Secret.Gifter.Application.Commands.ParticipantCommands
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
