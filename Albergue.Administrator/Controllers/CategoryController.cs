using Albergue.Administrator.Model;
using Albergue.Administrator.SQLite.Database.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PubSub;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Albergue.Administrator.Controllers
{
    [Authorize]
    [Route("api/shop/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _repository;
        private readonly ILogger<CategoryController> _logger;
        private readonly Hub _hub;

        private CategoryController()
        {
            _hub = Hub.Default;
        }

        public CategoryController(
            ILogger<CategoryController> logger,
            ICategoryRepository repository)
            : this()
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddCategoryAsync(Category item, CancellationToken cancellationToken)
        {
            try
            {
                var added = await _repository.AddAsync(item, cancellationToken);
                _hub.Publish(added);

                return Ok(added);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> UpdateCategoryAsync(Category item, CancellationToken cancellationToken)
        {
            try
            {
                var updated = await _repository.UpdateAsync(item, cancellationToken);
                _hub.Publish(updated);

                return Ok(updated);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(400);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DeleteCategoryAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(id, cancellationToken);

                if (deleted < 0)
                {
                    _hub.Publish(id);

                    return Ok();
                }

                return StatusCode(400);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(400);
            }
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetCategoryByIdAsync(string id, CancellationToken cancellationToken)
        {
            try
            {
                var found = await _repository.GetByIdAsync(id, cancellationToken);

                if (found == null)
                {
                    return NotFound();
                }

                return Ok(found);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(400);
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category[]))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllCategoriesAsync(CancellationToken cancellationToken)
        {
            try
            {
                var allOfThem = await _repository.GetAllAsync(cancellationToken);

                return Ok(allOfThem);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                return StatusCode(400);
            }
        }
    }
}
