using MediatR;

namespace Christmas.Secret.Gifter.Application.Commands.ParticipantCommands
{
    public record CheckIfNameAlreadyExistCommand : IRequest<bool>
    {
        public CheckIfNameAlreadyExistCommand(string eventId, string name)
        {
            EventId = eventId;
            Name = name;
        }

        public string EventId { get; private set; }
        public string Name { get; private set; }
    }
}
