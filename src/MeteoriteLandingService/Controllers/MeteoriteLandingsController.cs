using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Sage.MeteoriteLandingService.ControllerModels;
using Sage.MeteoriteLandingService.Services;

namespace Sage.MeteoriteLandingService.Controllers
{
    [ApiController]
    [Route("/meteoriteLanding")]
    public class MeteoriteLandingsController : ControllerBase
    {
        private readonly ILogger<MeteoriteLandingsController> _logger;
        private readonly IMeteoriteLandingService _service;
        private readonly IMapper _mapper;

        public MeteoriteLandingsController(ILogger<MeteoriteLandingsController> logger, IMeteoriteLandingService service, IMapper mapper)
        {
            _logger = logger;
            _service = service;
            _mapper = mapper;
        }

        [HttpGet("")]
        [ProducesResponseType(typeof(IEnumerable<MeteoriteLanding>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = _service.GetAsync();
            if (result == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Error { Message = "Failed to get meteorite landings" }
                );
            }

            var meteoriteLandings = new List<MeteoriteLanding>();
            await foreach (var meteoriteLanding in result)
            {
                meteoriteLandings.Add(_mapper.Map<MeteoriteLanding>(meteoriteLanding));
            }

            return Ok(meteoriteLandings);
        }

        [HttpGet("exciting-new-functionality")]
        [ProducesResponseType(typeof(IEnumerable<MeteoriteLanding>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetExcitingNewFunctionality()
        {
            return Ok("Yay heroku!");
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MeteoriteLanding), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _service.GetAsync(id);
            if (result == null)
            {
                return NotFound(new Error { Message = $"Meteorite landing {id} not found" });
            }

            var meteoriteLanding = _mapper.Map<MeteoriteLanding>(result);

            return Ok(meteoriteLanding);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(Guid id, MeteoriteLandingCreateOrUpdate meteoriteLandingUpdate)
        {
            var dbMeteoriteLandings = _mapper.Map<Sage.MeteoriteLandingService.Models.MeteoriteLandings>(meteoriteLandingUpdate);
            dbMeteoriteLandings.Id = id;
            var (updatedId, entityFound) = await _service.UpdateAsync(dbMeteoriteLandings);
            if (!entityFound)
            {
                return NotFound(new Error { Message = $"Meteorite landing {id} not found" });
            }
            if (updatedId == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Error { Message = $"Failed to updated meteorite landing with ID {id}" }
                );
            }

            return Ok(updatedId);
        }

        [HttpPost("")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(MeteoriteLandingCreateOrUpdate meteoriteLandingCreate)
        {
            var result = await _service.CreateAsync(
                meteoriteLandingCreate.Name,
                meteoriteLandingCreate.MeteoriteId,
                meteoriteLandingCreate.Nametype,
                meteoriteLandingCreate.Recclass,
                meteoriteLandingCreate.Mass,
                meteoriteLandingCreate.Fall,
                meteoriteLandingCreate.Year,
                meteoriteLandingCreate.Reclat,
                meteoriteLandingCreate.Reclong,
                meteoriteLandingCreate.GeoLocation
            );
            if (result == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Error { Message = $"Meteorite landing failed to be created" }
                );
            }

            return Created(Request.GetDisplayUrl() + "/" + result, result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Error), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var (deletedId, entityFound) = await _service.DeleteAsync(id);
            if (!entityFound)
            {
                return NotFound(new Error { Message = $"Meteorite landing {id} not found" });
            }
            if (deletedId == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    new Error { Message = $"Failed to delete meteorite landing with ID {id}" }
                );
            }

            return Ok(deletedId);
        }
    }
}
