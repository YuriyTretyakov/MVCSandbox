using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Whoops.DataLayer;

namespace Whoops.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IWorldRepository _repository;
        

        public AppController(IWorldRepository repository)
        {
            _repository = repository;
           
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Trips()
        {
            var data = _repository.GetAllTrips();
            return View(data);
        }

        public IActionResult Contacts()
        {
            return View();
        }

        
    }
}