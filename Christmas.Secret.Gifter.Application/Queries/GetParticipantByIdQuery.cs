using Christmas.Secret.Gifter.Domain;
using MediatR;

namespace Christmas.Secret.Gifter.Application.Queries
{
    public record GetParticipantByIdQuery : IRequest<Participant>
    {
        public GetParticipantByIdQuery(string participantId)
        {
            ParticipantId = participantId;
        }

        public string ParticipantId { get; set; }
    }
}
