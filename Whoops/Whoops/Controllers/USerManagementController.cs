using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Whoops.DataLayer;
using Whoops.ViewModels;

namespace Whoops.Controllers
{
    
    public class UserManagementController : Controller
    {
        private readonly UserManager<User> _userManager;

        public UserManagementController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserViewModel createUserView)
        {
            if (ModelState.IsValid)
            {
                if (createUserView.ConfirmPassword != createUserView.Password)
                    return BadRequest("Passwords doesn't match");

                var user = Mapper.Map<User>(createUserView);
                user.LastLoggedInOn = DateTime.Now;
                user.RegisteredOn = DateTime.Now;
                user.UserName = user.Email;
                var result=await _userManager.CreateAsync(user, createUserView.Password);

                if (result.Succeeded)
                    return Created($"/usermanagement/GetUserInfo/{user.Id}", user);

                return BadRequest(string.Join("\r\n",result.Errors.Select(e=>e.Description)));
            }

            
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpdateUser(string userId)
        {
            return View();
        }


        [Authorize]
        [HttpGet]
        public IActionResult GetUserInfo(string userId)
        {
            return View();
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
    }
}