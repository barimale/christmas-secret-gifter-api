using Christmas.Secret.Gifter.Application.Commands.ParticipantCommands;
using Christmas.Secret.Gifter.Application.Queries;
using Christmas.Secret.Gifter.Application.Services.Abstractions;
using Christmas.Secret.Gifter.Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Controllers
{
    [AllowAnonymous]
    [Route("api/v{version:apiVersion}")]
    [Route("api")]
    [ApiVersion("1.0")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly IParticipantService _participantService;
        private readonly IEventService _eventService;
        private readonly ILogger<ParticipantsController> _logger;
        private readonly IMediator _mediator;

        public ParticipantsController(
            ILogger<ParticipantsController> logger,
            IParticipantService participantService,
            IEventService eventService,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            _participantService = participantService;
            _eventService = eventService;
        }

        [HttpPost("events/{eventId}/[controller]/register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Participant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(
            string eventId,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] Participant input,
            CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existedEvent = await _mediator
                   .Send(new GetGiftEventByIdQuery(eventId),
                       cancellationToken);

                if (existedEvent == null)
                {
                    return NotFound("Event with such id not registered.");
                }

                var created = await _mediator
                   .Send(new AddParticipantCommand(input),
                       cancellationToken);

                return Ok(created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("events/{eventId}/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Participant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update(
            string eventId,
            string id,
            [FromBody(EmptyBodyBehavior = EmptyBodyBehavior.Allow)] Participant input,
            CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existedEvent = await _mediator
                  .Send(new GetGiftEventByIdQuery(eventId),
                      cancellationToken);

                if (existedEvent == null)
                {
                    return NotFound("Event with such id not registered.");
                }

                var existed = await _mediator
                  .Send(new GetParticipantByIdQuery(id),
                      cancellationToken);

                if (existed == null)
                {
                    return NotFound("Participant with such id not created.");
                }

                var updated = await _mediator
                  .Send(new UpdateParticipantCommand(input),
                      cancellationToken);

                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("events/{eventId}/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(int))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Remove(
           string eventId,
           string id,
           CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existedEvent = await _mediator
                  .Send(new GetGiftEventByIdQuery(eventId),
                      cancellationToken);

                if (existedEvent == null)
                {
                    return NotFound("Event with such id not registered.");
                }

                var deleted = await _mediator
                  .Send(new DeleteParticipantCommand(id),
                      cancellationToken);

                return Ok(deleted);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events/{eventId}/[controller]/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Participant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetById(
            string eventId,
            string id,
            CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existedEvent = await _mediator
                   .Send(new GetGiftEventByIdQuery(eventId),
                       cancellationToken);

                if (existedEvent == null)
                {
                    return NotFound("Event with such id not registered.");
                }

                var existed = await _mediator
                  .Send(new GetParticipantByIdQuery(id),
                      cancellationToken);

                if (existed == null)
                {
                    return NotFound("Participant with such id not created.");
                }

                return Ok(existed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events/{eventId}/[controller]/check-name-existance/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Participant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CheckIfNameAlreadyExist(
            string eventId,
            string name,
            CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existedEvent = await _mediator
                   .Send(new GetGiftEventByIdQuery(eventId),
                       cancellationToken);

                if (existedEvent == null)
                {
                    return NotFound("Event with such id not registered.");
                }

                var existed = await _mediator
                   .Send(new CheckIfNameAlreadyExistCommand(eventId, name),
                       cancellationToken);

                return Ok(existed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events/{eventId}/[controller]/check-email-existance/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Participant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CheckIfEmailAlreadyExist(
            string eventId,
            string email,
            CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existedEvent = await _mediator
                  .Send(new GetGiftEventByIdQuery(eventId),
                      cancellationToken);

                if (existedEvent == null)
                {
                    return NotFound("Event with such id not registered.");
                }

                var existed = await _mediator
                  .Send(new CheckIfEmailAlreadyExistCommand(eventId, email),
                      cancellationToken);

                return Ok(existed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events/{eventId}/[controller]/{participantId}/check-name-existance/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Participant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CheckIfNameAlreadyExistEditMode(
            string participantId,
            string eventId,
            string name,
            CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existedEvent = await _mediator
                  .Send(new GetGiftEventByIdQuery(eventId),
                      cancellationToken);

                if (existedEvent == null)
                {
                    return NotFound("Event with such id not registered.");
                }

                var existed = await _mediator
                  .Send(new CheckIfNameAlreadyExistEditModeCommand(
                      eventId,
                      participantId,
                      name),
                      cancellationToken);

                return Ok(existed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events/{eventId}/[controller]/{participantId}/check-email-existance/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Participant))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CheckIfEmailAlreadyExistEditMode(
            string participantId,
            string eventId,
            string email,
            CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existedEvent = await _mediator
                   .Send(new GetGiftEventByIdQuery(eventId),
                       cancellationToken);

                if (existedEvent == null)
                {
                    return NotFound("Event with such id not registered.");
                }

                var existed = await _mediator
                  .Send(new CheckIfEmailAlreadyExistEditModeCommand(
                        eventId,
                        participantId,
                        email),
                      cancellationToken);

                return Ok(existed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("events/{eventId}/[controller]/all")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Participant[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAll(
            string eventId,
            CancellationToken cancellationToken)
        {
            try
            {
                cancellationToken.ThrowIfCancellationRequested();

                var existedEvent = await _mediator
                  .Send(new GetGiftEventByIdQuery(eventId),
                      cancellationToken);

                if (existedEvent == null)
                {
                    return NotFound("Event with such id not registered.");
                }

                return Ok(existedEvent.Participants.ToArray());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
