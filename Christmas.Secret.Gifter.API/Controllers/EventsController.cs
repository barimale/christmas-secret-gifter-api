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
        private readonly ILogger<EventsController> _logger;

        public EventsController(ILogger<EventsController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Event))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create(CancellationToken cancellationToken)
        {
            try
            {
                return Ok(new Event());
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
                return Ok(new Event());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
