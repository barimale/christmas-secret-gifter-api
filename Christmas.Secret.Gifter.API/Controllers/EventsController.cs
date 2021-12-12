using AutoMapper;
using Christmas.Secret.Gifter.API.Services.Abstractions;
using Christmas.Secret.Gifter.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IEventService _eventService;
        private readonly ILogger<EventsController> _logger;

        public EventsController(
            ILogger<EventsController> logger,
            IMapper mapper,
            IEventService eventService)
        {
            _logger = logger;
            _mapper = mapper;
            _eventService = eventService;
        }

        [HttpPost("[controller]/[action]")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var newEvent = new Event()
                {
                    EventId = Guid.NewGuid().ToString()
                };

                var created = await _eventService.AddAsync(newEvent, cancellationToken);

                return Ok(created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[controller]/{eventId}/execute")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Execute(string eventId, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var result = await _eventService.ExecuteAsync(eventId, cancellationToken);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existed = await _eventService.GetByIdAsync(id, cancellationToken);

                if(existed == null)
                {
                    return NotFound();
                }

                return Ok(existed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
