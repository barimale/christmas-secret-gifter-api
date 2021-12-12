namespace Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions.Scoped
{
    public interface IBaseRepositoryInnerScope<T, U>
        where T : class
        where U : class
    {
        Task<int> DeleteAsync(U id, CancellationToken cancellationToken);
        Task<T[]> GetAllAsync(CancellationToken? cancellationToken = null);
        Task<T> UpdateAsync(T item, CancellationToken cancellationToken);
    }
}