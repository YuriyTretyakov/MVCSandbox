using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Whoops.DataLayer;
using Whoops.Services;
using Whoops.ViewModels;
using Whoops.ViewModels.User;

namespace Whoops.Controllers
{
    
    public class UserManagementController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;
        private readonly EmailSender _sender;
       // private readonly ViberMessanger _viber;

        public UserManagementController(UserManager<User> userManager, 
                                        RoleManager<IdentityRole> roleManager,
                                        SignInManager<User> signInManager,
                                        EmailSender sender)
           //                             ViberMessanger viber)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _sender = sender;
         //   _viber = viber;
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
        public IActionResult GetUserInfo()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public  async Task<IActionResult> GetUserInfoData()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return Ok(Mapper.Map<UserInfoViewModel>(user));
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
                    await _signInManager.PasswordSignInAsync(user.UserName, createUserView.Password, true, false);

                  //  var emailConfirmToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                  //  string confirmationLink = Url.Action("ConfirmEmail","Account", 
                                                    //      new{userid = user.Id,
                                                        //      token = emailConfirmToken
                                                      //    },protocol: HttpContext.Request.Scheme);

                 //   await _viber.PostMessage("some text", "+380997151922");

                   
                //    _sender.SendConfirmation(user.Email, confirmationLink, "SomeFunWebSite: Confirm your email to receive notifications");

                    return Created($"/usermanagement/GetUserInfo", Mapper.Map<UserInfoViewModel>(user));
                }

                return BadRequest(string.Join("\r\n", result.Errors.Select(e => e.Description)));
            }
            return BadRequest(ModelState);
        }



    }
}