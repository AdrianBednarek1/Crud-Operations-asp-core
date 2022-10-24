using Microsoft.AspNetCore.Mvc;
using MVC.project.ViewModels.MakeViewModels;
using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore.VehicleDB;
using Microsoft.EntityFrameworkCore;
namespace MVC.project.Controllers
{
    public class MakeController : Controller
    {
        private IVehicleServiceMake vehicleServiceMake;
        public MakeController(IVehicleServiceMake _vehicleServiceMake)
        {
            vehicleServiceMake = _vehicleServiceMake;
        }
        public async Task<IActionResult> VehicleMake()
        {
            MakeListViewModel makeList = new MakeListViewModel();
            makeList.vehicleMakeList = await vehicleServiceMake.GetVehicleMakes();
            return View(makeList);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            VehicleMake vehicleMake = await vehicleServiceMake.SearchVehicleMake(id);
            await vehicleServiceMake.Delete(vehicleMake);
            return RedirectToAction("VehicleMake");
        }
        [HttpGet]
        public IActionResult CreateMake()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMake(MakeViewModel makeViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            VehicleMake vehicleMake = new VehicleMake()
            {
                Name = makeViewModel.Name,
                Abrv = makeViewModel.Abrv
            };
            await vehicleServiceMake.Create(vehicleMake);
            return RedirectToAction("VehicleMake");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateMake(int id)
        {
            VehicleMake vehicleMake = await vehicleServiceMake.SearchVehicleMake(id);           

            return View(vehicleMake);
        }
        [HttpPost]
        public async Task<IActionResult>UpdateMake(VehicleMake vehicleMake)
        {
            if (!ModelState.IsValid)
            {
                return View(vehicleMake);
            }
            await vehicleServiceMake.Update(vehicleMake);
            return RedirectToAction("VehicleMake");
        }
    }
}
