namespace Christmas.Secret.Gifter.Infrastructure.Repositories.Abstractions.Scoped
{
    public interface IBaseRepositoryOuterScope<T, U>
        where T : class
        where U : class
    {
        Task<T> AddAsync(T item, CancellationToken cancellationToken);
        Task<T> GetByIdAsync(U id, CancellationToken cancellationToken);
    }
}