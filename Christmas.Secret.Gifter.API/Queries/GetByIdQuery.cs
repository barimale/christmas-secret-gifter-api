using Christmas.Secret.Gifter.Domain;
using MediatR;

namespace Christmas.Secret.Gifter.API.Queries
{
    public record GetByIdQuery : IRequest<GiftEvent>
    {
        public GetByIdQuery(string giftEventId)
        {
            GiftEventId = giftEventId;
        }

        public string GiftEventId { get; set; }
    }
}
