using Algorithm.ConstraintsPairing.Model.Requests;
using Christmas.Secret.Gifter.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Christmas.Secret.Gifter.API.Controllers
{
    [AllowAnonymous]
    [Route("api")]
    [ApiController]
    public class ParticipantsController : ControllerBase
    {
        private readonly ILogger<ParticipantsController> _logger;

        public ParticipantsController(ILogger<ParticipantsController> logger)
        {
            _logger = logger;
        }

        [HttpPost("events/{eventId}/[controller]/register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Participant[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Register(
            string eventId,
            [FromBody] Participant[] input,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(new List<Participant>().ToArray());
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
            int id,
            CancellationToken cancellationToken)
        {
            try
            {
                return Ok(new Participant());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}
