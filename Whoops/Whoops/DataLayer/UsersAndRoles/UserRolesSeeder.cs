using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Whoops.DataLayer.UserInfo;

namespace Whoops.DataLayer.UsersAndRoles
{
    public class UserRolesSeeder
    {
        UserManager<User> _userManager;
        RoleManager<IdentityRole> _roleManager;
        UserInfoContext _context;

        public UserRolesSeeder(UserInfoContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Seed()
        {
            if (!await _roleManager.RoleExistsAsync("Administrator"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Administrator" });

            if (!await _roleManager.RoleExistsAsync("Worker"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Worker" });

            if (!await _roleManager.RoleExistsAsync("Customer"))
                await _roleManager.CreateAsync(new IdentityRole { Name = "Customer" });


            string seedUserName = "admin@site.com";

            if (await _userManager.FindByEmailAsync(seedUserName) == null)
            {
                var user = new User
                {
                    UserName = seedUserName,
                    Email = seedUserName
                };

                IdentityResult result = await _userManager.CreateAsync(user, "P@ssword");

                if (result.Succeeded)
                    await _userManager.AddToRolesAsync(user, new[] { "Administrator", "Worker" });

            }
        }

    }
}
