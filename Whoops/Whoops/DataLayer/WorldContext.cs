using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Whoops.DataLayer.UserInfo;

namespace Whoops.DataLayer
{
    public class WorldContext:IdentityDbContext<User>
    {
        private readonly IConfigurationRoot _config;

        public WorldContext(IConfigurationRoot config, DbContextOptions<WorldContext> options) :base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnection"]);
        }


        public DbSet<Trip> Trips { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<UserHistory> UserHistory { get; internal set; }
        public DbSet<Loyalty> Loyalty { get; internal set; }
    }
    
}
