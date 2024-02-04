using MediatR;

namespace Christmas.Secret.Gifter.API.Queries
{
    public record CheckIfNameAlreadyExistEditModeCommand : IRequest<bool>
    {
        public CheckIfNameAlreadyExistEditModeCommand(string eventId, string participantId, string name)
        {
            EventId = eventId;
            ParticipantId = participantId;
            Name = name;
        }

        public string EventId { get; private set; }
        public string ParticipantId { get; private set; }
        public string Name { get; private set; }
    }
}
