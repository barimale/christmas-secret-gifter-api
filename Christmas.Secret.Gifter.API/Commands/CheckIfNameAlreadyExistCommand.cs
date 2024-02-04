using MediatR;

namespace Christmas.Secret.Gifter.API.Queries
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
