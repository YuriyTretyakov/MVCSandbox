using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Whoops.ViewModels;

namespace Whoops.Controllers
{
    
    public class UserManagementController : Controller
    {
        
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
                return View();
            }

            ModelState.AddModelError("","Invalid data");
            return BadRequest(ModelState);
        }

        [Authorize]
        [HttpGet]
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