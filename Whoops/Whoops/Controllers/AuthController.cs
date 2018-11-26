using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Whoops.DataLayer;
using Whoops.ViewModels;

namespace Whoops.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<WorldUser> _signInManager;

        public AuthController(SignInManager<WorldUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("Trips", "App");

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login( LoginViewModel login,string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(login.Username, login.Password, true, false);

                if (signInResult.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl))
                        return RedirectToAction("Trips", "App");
                    else
                        return Redirect(returnUrl);
                }
                else
                {
                    ModelState.AddModelError("", "USername or password is incorrect");
                }

            }





            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
    }
}