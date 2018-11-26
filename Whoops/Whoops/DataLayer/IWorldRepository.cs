using System.Collections.Generic;
using System.Threading.Tasks;
using Whoops.ViewModels.Index;

namespace Whoops.DataLayer
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();
        void AddTrip(Trip trip);
        Task<bool> SaveChangesAsync();
        Trip GetTripByName(string tripname);
        void AddStop(string tripName, Stop stop);
    }
}