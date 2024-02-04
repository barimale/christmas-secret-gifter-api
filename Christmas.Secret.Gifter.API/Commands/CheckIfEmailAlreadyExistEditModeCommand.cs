using MediatR;

namespace Christmas.Secret.Gifter.API.Queries
{
    public record CheckIfEmailAlreadyExistEditModeCommand : IRequest<bool>
    {
        public CheckIfEmailAlreadyExistEditModeCommand(string eventId, string participantId, string email)
        {
            EventId = eventId;
            ParticipantId = participantId;
            Email = email;
        }

        public string EventId { get; private set; }
        public string ParticipantId { get; private set; }
        public string Email { get; private set; }
    }
}
