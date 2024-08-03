using Algorithm.ConstraintsPairing.Model.Responses;
using Christmas.Secret.Gifter.Domain;
using Christmas.Secret.Gifter.Infrastructure.Repositories.Abstractions.Scoped;

namespace Christmas.Secret.Gifter.Application.Services.Abstractions
{
    public interface IEventService : IBaseRepositoryOuterScope<GiftEvent, string>
    {
        Task<AlgorithmResponse> ExecuteAsync(GiftEvent giftEvent, CancellationToken cancellationToken = default);
    }
}
