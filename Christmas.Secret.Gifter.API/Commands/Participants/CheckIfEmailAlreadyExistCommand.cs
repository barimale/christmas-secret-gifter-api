using MediatR;

namespace Christmas.Secret.Gifter.API.Commands.Participants
{
    public record CheckIfEmailAlreadyExistCommand : IRequest<bool>
    {
        public CheckIfEmailAlreadyExistCommand(string eventId, string email)
        {
            EventId = eventId;
            Email = email;
        }

        public string EventId { get; private set; }
        public string Email { get; private set; }
    }
}
