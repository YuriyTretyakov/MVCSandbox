using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Whoops.DataLayer.UserInfo;

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

        public void AddUserHistory(UserHistory history)
        {
            _context.UserHistory.Add(history);

        }

        public List<UserHistory> GetUserHistory(string userId)
        {
            return _context.UserHistory.Where(h => h.UserId == userId).ToList();
        }

        public void UpdateCustomerFeedback(FeedBack custFeedback, string userId, int historyId)
        {
            var concreteHistoryItem = _context.UserHistory.FirstOrDefault(h => h.HistoryId == historyId && h.UserId == userId);
            concreteHistoryItem.CustomerFeedback = custFeedback;
        }

        public void UpdateOperationistFeedback(FeedBack opsFeedback, string userId, int historyId)
        {
            var concreteHistoryItem = _context.UserHistory.FirstOrDefault(h => h.HistoryId == historyId && h.UserId == userId);
            concreteHistoryItem.OperationistFeedback = opsFeedback;
        }

        public void RemoveSeededUser()
        {
           // WorldContextSeedData.SeedUseName
        }


    }
}
