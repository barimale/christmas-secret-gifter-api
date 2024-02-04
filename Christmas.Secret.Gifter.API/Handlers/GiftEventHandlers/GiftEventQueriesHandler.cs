using Christmas.Secret.Gifter.API.Queries;
using Christmas.Secret.Gifter.Domain;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Christmas.Secret.Gifter.API.Services.Abstractions;

namespace Christmas.Secret.Gifter.API.Handlers.GiftEventHandlers
{
    internal sealed class GiftEventQueriesHandler :
        IRequestHandler<GetGiftEventByIdQuery, GiftEvent>
    {
        private readonly IEventService _eventService;
        public GiftEventQueriesHandler(IEventService eventService)
            => _eventService = eventService;

        public Task<GiftEvent> Handle(GetGiftEventByIdQuery request, CancellationToken cancellationToken)
        {
            return _eventService.GetByIdAsync(request.GiftEventId, cancellationToken);
        }
    }
}
