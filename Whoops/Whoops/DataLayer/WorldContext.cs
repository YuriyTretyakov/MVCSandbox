using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Whoops.DataLayer
{
    public class WorldContext:IdentityDbContext<User>
    {
        private readonly IConfigurationRoot _config;

        public WorldContext(IConfigurationRoot config, DbContextOptions options) :base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnection"]);
        }


        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }

    }
    
}
