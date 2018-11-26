using System;
using System.Collections;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Whoops.DataLayer;
using Whoops.ViewModels.Index;

namespace Whoops.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private readonly IWorldRepository _repository;
        private readonly ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repository,ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var data = Mapper.Map<IEnumerable<TripViewModel>>(_repository.GetAllTrips());
                return Ok(data);
            }
            catch (Exception e)
            {
                _logger.LogError("error occured at attempt to get trips",e.Message);
                return BadRequest($"Error occured at attempt to get trips {e.Message}");
            }
           
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody]TripViewModel trip)
        {
            if (ModelState.IsValid)
            {
                var tripData = Mapper.Map<Trip>(trip);

                _repository.AddTrip(tripData);

                if (await _repository.SaveChangesAsync())
                    return Created($"/api/trips/{trip.Name}", Mapper.Map<TripViewModel>(tripData));
                else
                    return BadRequest("Failed to save trip");
            }

            return BadRequest(ModelState);

        }
    }
}