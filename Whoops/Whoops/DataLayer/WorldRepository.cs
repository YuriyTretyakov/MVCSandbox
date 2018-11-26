using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Whoops.DataLayer
{
    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext _context;
        private readonly ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context,ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting all trips from the database");
            return _context.Trips;
        }

        public void AddTrip(Trip trip)
        {
            _context.Trips.Add(trip);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return ((await _context.SaveChangesAsync()) > 0);
        }

        public Trip GetTripByName(string tripname)
        {
            return _context.Trips.Include(t=>t.Stops)
                                 .FirstOrDefault(t => t.Name.Equals(tripname, StringComparison.OrdinalIgnoreCase));
        }

        public void AddStop(string tripName, Stop stop)
        {
            var trip = GetTripByName(tripName);
            trip.Stops.Add(stop);
            _context.Stops.Add(stop);
        }
    }
}
