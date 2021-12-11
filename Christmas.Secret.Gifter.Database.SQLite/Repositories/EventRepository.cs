using AutoMapper;
using Christmas.Secret.Gifter.Database.SQLite.Entities;
using Christmas.Secret.Gifter.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Christmas.Secret.Gifter.Database.SQLite.SQLite.Database.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly IMapper _mapper;
        private readonly GifterDbContext _context;
        private readonly ILogger<EventRepository> _logger;

        public EventRepository(
            ILogger<EventRepository> logger,
            GifterDbContext context,
            IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        public async Task<Event> AddAsync(Event item, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var mapped = _mapper.Map<EventEntry>(item);
                var result = await _context.Events.AddAsync(mapped, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var mappedResult = _mapper.Map<Event>(result.Entity);

                return mappedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Adding failed", ex);
            }
        }

        public async Task<Event> UpdateAsync(Event item, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var mapped = _mapper.Map<EventEntry>(item);

                var existed = await _context
                   .Events
                   .Include(p => p.Participants)
                   .AsQueryable()
                   .FirstOrDefaultAsync(p => p.Id == item.Id, cancellationToken);

                await _context.SaveChangesAsync(cancellationToken);

                var result = _context.Update(existed);

                await _context.SaveChangesAsync(cancellationToken);

                var mappedResult = _mapper.Map<Event>(result.Entity);

                return mappedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Updating failed", ex);
            }
        }

        public async Task<int> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existed = await _context.Events.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                var deleted = _context
                    .Events
                    .Remove(existed);

                var result = await _context.SaveChangesAsync(cancellationToken);

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw new SystemException("Deleting failed", ex);
            }
        }

        public async Task<Event> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var found = await _context
                    .Events
                    .Include(p => p.Participants)
                    .AsQueryable()
                    .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

                if (found == null)
                {
                    return null;
                }

                var mappedResult = _mapper.Map<Event>(found);

                return mappedResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }

        public async Task<Event[]> GetAllAsync(CancellationToken? cancellationToken)
        {
            try
            {
                cancellationToken?.ThrowIfCancellationRequested();

                var allOfThem = await _context
                    .Events
                    .Include(p => p.Participants)
                    //.ThenInclude(pp => pp.Language)
                    .ToArrayAsync(cancellationToken ?? default);

                var mapped = allOfThem.Select(p => _mapper.Map<Event>(p));

                return mapped.ToArray();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }

            return null;
        }
    }
}
