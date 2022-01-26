using Algorithm.ConstraintsPairing;
using AutoMapper;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Christmas.Secret.Gifter.Database.SQLite.Entries;
using Christmas.Secret.Gifter.Database.SQLite.Repositories.Abstractions;
using Christmas.Secret.Gifter.Domain;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Services
{
    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;
        private readonly Engine _engine;
        private readonly IEventRepository _eventRepoistory;
        private readonly IMapper _mapper;

        public EventService(ILogger<EventService> logger, IEventRepository eventRepoistory, IMapper mapper)
        {
            _engine = new Engine();
            _logger = logger;
            _eventRepoistory = eventRepoistory;
            _mapper = mapper;
        }

        public async Task<Event> AddAsync(Event item, CancellationToken cancellationToken)
        {
            var mapped = _mapper.Map<EventEntry>(item);
            var added = await _eventRepoistory.AddAsync(mapped, cancellationToken);

            return _mapper.Map<Event>(added);
        }

        public async Task<Event> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            var found = await _eventRepoistory.GetByIdAsync(id, cancellationToken);

            return _mapper.Map<Event>(found);
        }

        public async Task ExecuteAsync(string eventId = null, CancellationToken cancellationToken = default)
        {
            try
            {
                if (eventId == null)
                {
                    throw new System.Exception("EventId is not provided. Create the new event first.");
                }

                var existed = await _eventRepoistory.GetByIdAsync(eventId, cancellationToken);

                switch (existed.State)
                {
                    case EventState.CREATED:
                        return;
                    case EventState.READY_FOR_ANALYZE:
                        //existed.State = EventState.ANALYZE_IN_PROGRESS;
                        //await _eventRepoistory.UpdateAsync(existed, cancellationToken);
                        //var inputData = _eventRepoistory
                        //var result = await _engine.CalculateAsync(input.ToInputData());

                        //return new AlgorithmResponse()
                        //{
                        //    IsError = result.IsError,
                        //    Reason = result.Reason,
                        //    Pairs = result.Data.Pairs,
                        //    AnalysisStatus = result.Data.Status.ToString()
                        //});
                        return;
                    case EventState.ANALYZE_IN_PROGRESS:
                        return;
                    case EventState.COMPLETED_SUCCESSFULLY:
                        return;
                    case EventState.COMPLETED_FAILY:
                        return;
                    case EventState.ABANDONED:
                        return;
                    default:
                        return;
                }
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        Task<Event> IEventService.ExecuteAsync(string eventId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
