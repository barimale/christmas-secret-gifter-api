using Albergue.Administrator.Model;
using Albergue.Administrator.SQLite.Database.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Albergue.Administrator.Controllers
{
    [AllowAnonymous]
    [Route("api/shop/[controller]/[action]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IItemRepository _repository;
        private readonly ILogger<StatusController> _logger;

        public StatusController(
            ILogger<StatusController> logger,
            IItemRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShopStatus))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetStatusAsync(CancellationToken cancellationToken)
        {
            try
            {
                var allOfThem = await _repository.GetAllAsync(cancellationToken);
                

                return Ok(new ShopStatus {
                    isAtLeastOneCategoryDefined = allOfThem.ToList().Any(p => p.Active)
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(400);
            }
        }
    }
}
