using MediatR;
using Algorithm.ConstraintsPairing.Model.Responses;
using Christmas.Secret.Gifter.Domain;
using Algorithm.ConstraintsPairing;
using Algorithm.ConstraintsPairing.Model;
using Christmas.Secret.Gifter.Application.Services.Abstractions;
using Christmas.Secret.Gifter.Application.Commands.GiftEventCommands;

namespace Christmas.Secret.Gifter.Application.Handlers.GiftEventHandlers
{
    internal sealed class GiftEventCommandsHandler :
        IRequestHandler<ExecuteEngineCommand, AlgorithmResponse>,
        IRequestHandler<AddGiftEventCommand, GiftEvent>,
        IRequestHandler<CalculateCommand, OutputDataSummary>
    {
        private readonly IEventService _eventService;
        private readonly Engine _engine;

        public GiftEventCommandsHandler()
        {
            _engine = new Engine();
        }

        public GiftEventCommandsHandler(IEventService eventService) : this()
            => _eventService = eventService;

        public Task<AlgorithmResponse> Handle(ExecuteEngineCommand request, CancellationToken cancellationToken)
        {
            return _eventService.ExecuteAsync(request.GiftEvent, cancellationToken);
        }

        public Task<GiftEvent> Handle(AddGiftEventCommand request, CancellationToken cancellationToken)
        {
            return _eventService.AddAsync(request.GiftEvent, cancellationToken);
        }

        public Task<OutputDataSummary> Handle(CalculateCommand request, CancellationToken cancellationToken)
        {
            return _engine.CalculateAsync(request.Request.ToInputData());

        }
    }
}
