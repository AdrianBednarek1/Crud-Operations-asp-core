using Microsoft.AspNetCore.Mvc;
using Project.MVC.Models;
using System.Diagnostics;

namespace Project.MVC.Controllers
{
    public class MakeController : Controller
    {
        private readonly ILogger<MakeController> _logger;

        public MakeController(ILogger<MakeController> logger)
        {
            _logger = logger;
        }

        public IActionResult VehicleMakeView()
        {
            return View();
        }

    }
}