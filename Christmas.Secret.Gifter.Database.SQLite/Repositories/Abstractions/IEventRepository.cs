using Christmas.Secret.Gifter.Infrastructure.Entities;

namespace Christmas.Secret.Gifter.Infrastructure.Repositories.Abstractions
{
    public interface IEventRepository : IBaseRepository<EventEntry, string>
    {
        //intentionally left blank
    }
}