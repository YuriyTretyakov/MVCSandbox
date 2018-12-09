using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Whoops.DataLayer;
using Whoops.ViewModels;
using Whoops.ViewModels.User;

namespace Whoops.Controllers
{
    
    public class UserManagementController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserManagementController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel createUserView)
        {
            return await createUserWithRole(createUserView, "Customer");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateWorker([FromBody] CreateUserViewModel createUserView)
        {
            return await createUserWithRole(createUserView, "Worker");
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> CreateAdministrator([FromBody] CreateUserViewModel createUserView)
        {
            return await createUserWithRole(createUserView, "Administrator");
        }


        [Authorize]
        [HttpPost]
        public IActionResult UpdateUser(string userId)
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public  async Task<IActionResult> GetUserInfo()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(Mapper.Map<UserInfoViewModel>(user));
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangePassword(string userId, string oldPassword,string newPassword)
        {
            return View();
        }

        [Authorize]
        [HttpDelete]
        public IActionResult Delete(string userId)
        {
            return View();
        }

        private async Task<IActionResult> createUserWithRole(CreateUserViewModel createUserView,params string[] roles)
        {
            if (ModelState.IsValid)
            {
                if (createUserView.ConfirmPassword != createUserView.Password)
                    return BadRequest("Passwords doesn't match");

                var user = Mapper.Map<User>(createUserView);
                user.LastLoggedInOn = DateTime.Now;
                user.RegisteredOn = DateTime.Now;
                user.UserName = user.Email;
                var result = await _userManager.CreateAsync(user, createUserView.Password);


                if (result.Succeeded)
                {
                    await _userManager.AddToRolesAsync(user, roles);
                    return Created($"/usermanagement/GetUserInfo/{user.Id}", Mapper.Map<UserInfoViewModel>(user));
                }

                return BadRequest(string.Join("\r\n", result.Errors.Select(e => e.Description)));
            }
            return BadRequest(ModelState);
        }



    }
}