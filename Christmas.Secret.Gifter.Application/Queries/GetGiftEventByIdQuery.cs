using Christmas.Secret.Gifter.Domain;
using MediatR;

namespace Christmas.Secret.Gifter.Application.Queries
{
    public record GetGiftEventByIdQuery : IRequest<GiftEvent>
    {
        public GetGiftEventByIdQuery(string giftEventId)
        {
            GiftEventId = giftEventId;
        }

        public string GiftEventId { get; set; }
    }
}
