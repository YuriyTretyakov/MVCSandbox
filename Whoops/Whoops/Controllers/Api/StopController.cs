using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Whoops.DataLayer;
using Whoops.Services;
using Whoops.ViewModels.Index;

namespace Whoops.Controllers.Api
{
    [Route("/api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private readonly IWorldRepository _repository;
        private readonly ILogger<StopController> _logger;
        private readonly GeoCoordServices _geoService;

        public StopController(IWorldRepository repository,ILogger<StopController> logger,GeoCoordServices geoService)
        {
            _repository = repository;
            _logger = logger;
            _geoService = geoService;
        }

        [HttpGet()]
        public IActionResult Get(string tripName)
        {
            try
            {
                var result = _repository.GetTripByName(tripName);
                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(result.Stops.OrderBy(o=>o.Order)));
            }
            catch (Exception e)
            {
                _logger.LogError($"Error occured :{e.Message}");
            }

            return BadRequest($"Failed to get stops by tripname {tripName}");
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(string tripName,[FromBody] StopViewModel  stop)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var data = Mapper.Map<Stop>(stop);
                    var coords = await _geoService.GetCoords(data.Name);

                    if (coords.Result)
                    {
                        data.Latitude = coords.Latitude;
                        data.Longtitude = coords.Longtitude;

                        _repository.AddStop(tripName, data);

                        if (await _repository.SaveChangesAsync())
                            return Created($"/api/trips/{tripName}/stops/{data.Name}", Mapper.Map<StopViewModel>(data));
                    }
                    else
                    {
                        return NotFound(coords.Message);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Error at attempt to save into db {e.Message}");
            }

            
            return BadRequest();
        }
    }
}