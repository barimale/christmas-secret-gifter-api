using Christmas.Secret.Gifter.API.Queries;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Algorithm.ConstraintsPairing.Model.Responses;
using Christmas.Secret.Gifter.Domain;

namespace Christmas.Secret.Gifter.API.Handlers
{
    internal sealed class GiftEventCommandsHandler :
        IRequestHandler<ExecuteEngineCommand, AlgorithmResponse>,
        IRequestHandler<AddGiftEventCommand, GiftEvent>
    {
        private readonly IEventService _eventService;
        public GiftEventCommandsHandler(IEventService eventService)
            => _eventService = eventService;

        public Task<AlgorithmResponse> Handle(ExecuteEngineCommand request, CancellationToken cancellationToken)
        {
            return _eventService.ExecuteAsync(request.GiftEvent, cancellationToken);
        }

        public Task<GiftEvent> Handle(AddGiftEventCommand request, CancellationToken cancellationToken)
        {
            return _eventService.AddAsync(request.GiftEvent, cancellationToken);
        }
    }
}
