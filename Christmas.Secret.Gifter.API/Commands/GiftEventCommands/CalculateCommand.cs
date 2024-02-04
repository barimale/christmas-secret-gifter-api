using Algorithm.ConstraintsPairing.Model;
using Algorithm.ConstraintsPairing.Model.Requests;
using MediatR;

namespace Christmas.Secret.Gifter.API.Commands.GiftEventCommands
{
    public record CalculateCommand : IRequest<OutputDataSummary>
    {
        public CalculateCommand(AlgorithmRequest request)
        {
            Request = request;
        }

        public AlgorithmRequest Request { get; private set; }
    }
}
