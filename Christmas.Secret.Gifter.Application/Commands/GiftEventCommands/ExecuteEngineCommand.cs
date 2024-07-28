using Algorithm.ConstraintsPairing.Model.Responses;
using Christmas.Secret.Gifter.Domain;
using MediatR;

namespace Christmas.Secret.Gifter.Application.Commands.GiftEventCommands
{
    public record ExecuteEngineCommand : IRequest<AlgorithmResponse>
    {
        public ExecuteEngineCommand(GiftEvent giftEvent)
        {
            GiftEvent = giftEvent;
        }

        public GiftEvent GiftEvent { get; private set; }
    }
}
