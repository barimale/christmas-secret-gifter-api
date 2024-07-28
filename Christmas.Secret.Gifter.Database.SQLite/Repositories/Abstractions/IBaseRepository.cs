using Christmas.Secret.Gifter.Infrastructure.Repositories.Abstractions.Scoped;

namespace Christmas.Secret.Gifter.Infrastructure.Repositories.Abstractions
{
    public interface IBaseRepository<T, U> : IBaseRepositoryInnerScope<T, U>, IBaseRepositoryOuterScope<T, U>
        where T : class
        where U : class
    {
        // intentionally left blank
    }
}