using Microsoft.AspNetCore.Mvc;

namespace Project.MVC.Controllers
{
    public class ModelController : Controller
    {
        public IActionResult VehicleModelView()
        {
            return View();
        }
    }
}
