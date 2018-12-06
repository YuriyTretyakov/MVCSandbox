using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Whoops.DataLayer.UserInfo
{
    public class UserInfoContext: IdentityDbContext<User,IdentityRole,string>
    {
        private IConfigurationRoot _config;

        public DbSet<UserHistory> UserHistory { get; set; }
        public DbSet<Loyalty> Loyalty { get; set; }
        

        public UserInfoContext(IConfigurationRoot config, DbContextOptions<UserInfoContext> options)  :base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config["ConnectionStrings:WorldContextConnection"]);
        }
    }
}
