using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Whoops.DataLayer
{
    public class WorldContextSeedData
    {
        private readonly WorldContext _context;
        private readonly UserManager<User> _userManager;

        public WorldContextSeedData(WorldContext context,UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task EnsureSeedData()
        {
            string seedUserName = "admin";


            if (await _userManager.FindByEmailAsync("admin@site.com") ==null)
            {
                var user = new User
                {
                    UserName = seedUserName,
                    Email = "admin@site.com"
                };

                IdentityResult result=await _userManager.CreateAsync(user, "P@ssword");
            }


            if (!_context.Trips.Any())
            {
                var trip1 = new Trip
                {
                    UserName = seedUserName,
                    DateCreated = DateTime.UtcNow,
                    Name = "Trip1",
                    Stops = new List<Stop>
                    {
                        new Stop{Name="Stop 1",Order=0,Longtitude=50.85,Latitude=33.12,Arrival=new DateTime(2018,11,18)},
                        new Stop{Name="Stop 2",Order=1,Longtitude=60.85,Latitude=34.12,Arrival=new DateTime(2018,11,19)}
                    }
                };

                _context.Trips.Add(trip1);
                _context.Stops.AddRange(trip1.Stops);
                await _context.SaveChangesAsync();

                var trip2 = new Trip
                {
                    UserName = seedUserName,
                    DateCreated = DateTime.UtcNow,
                    Name = "Trip2",
                    Stops = new List<Stop>
                    {
                        new Stop{Name="Stop 3",Order=0,Longtitude=49.021,Latitude=31.12,Arrival=new DateTime(2018,11,20)},
                        new Stop{Name="Stop 4",Order=1,Longtitude=53.125,Latitude=32.42,Arrival=new DateTime(2018,11,21)}
                    }
                };

                _context.Trips.Add(trip2);
                _context.Stops.AddRange(trip2.Stops);
                await _context.SaveChangesAsync();
            }
        }
    }
}
