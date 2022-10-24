using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.project.ViewModels.ModelViewModels;
using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore.VehicleDB;
using Microsoft.EntityFrameworkCore;

namespace MVC.project.Controllers
{
    public class ModelController : Controller
    {
        private IVehicleServiceModel vehicleServiceModel;
        private IVehicleServiceMake vehicleServiceMake;
        private List<SelectListItem> VehicleMakeInList;
        public ModelController
            (
            IVehicleServiceModel _vehicleServiceModel,
            IVehicleServiceMake _vehicleServiceMake
            )
        {
            vehicleServiceModel = _vehicleServiceModel;
            vehicleServiceMake = _vehicleServiceMake;
        }
        public async Task<IActionResult> VehicleModel()
        {
            ModelListViewModel modellistViewModel = new ModelListViewModel();
            modellistViewModel.vehicleModelList = await vehicleServiceModel.GetVehicleModels();
            ViewBag.VehicleMakeIsNull = vehicleServiceMake.VehicleMakeIsNull();                      
            return View(modellistViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> CreateModel()
        {
            await RefreshDropDown();
            return View();
        }   
        [HttpPost]
        public async Task<IActionResult> CreateModel(ModelViewModel modelViewModel)
        {
            await RefreshDropDown();
            if (!ModelState.IsValid)
            {
                return View();
            }
            VehicleModel vehicleModel = new VehicleModel()
            {
                Name = modelViewModel.Name,
                Abrv = modelViewModel.Abrv,
                MakeId = modelViewModel.MakeId
            };
            await vehicleServiceModel.Create(vehicleModel);
            return RedirectToAction("VehicleModel");
        }

        private async Task RefreshDropDown()
        {
            List<VehicleMake> listMake = new List<VehicleMake>();
            listMake = await vehicleServiceMake.GetVehicleMakes();
            VehicleMakeInList = listMake.ConvertAll(a =>
            {
                return new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                };
            });
            ViewBag.VehicleMakeInList = VehicleMakeInList;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            VehicleModel vehicleModel = await vehicleServiceModel.SearchVehicleModel(id);
            await vehicleServiceModel.Delete(vehicleModel);
            return RedirectToAction("VehicleModel");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateModel(int id)
        {
            VehicleModel vehicleModel = await vehicleServiceModel.SearchVehicleModel(id);
            await RefreshDropDown();
            return View(vehicleModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateModel(VehicleModel vehicleModel)
        {
            await RefreshDropDown();
            if (ModelState.ErrorCount!=1)
            {
                return View(vehicleModel);
            }
            await vehicleServiceModel.Update(vehicleModel);
            return RedirectToAction("VehicleModel");
        }
    }
}
