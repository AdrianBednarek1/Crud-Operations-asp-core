using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.project.ViewModels.ModelViewModels;
using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore.VehicleDB;
using Microsoft.EntityFrameworkCore;
using ZaPrav.NetCore;
using Project.Service.PagingSortingFiltering.PSFmodel;
using Project.Service;

namespace MVC.project.Controllers
{
    public class ModelController : Controller
    {
        private IVehicleServiceModel vehicleServiceModel;
        private IVehicleServiceMake vehicleServiceMake;
        private List<SelectListItem> VehicleMakeInList;

        public string? CurrentSearchModel { get; set; }
        public string? CurrentSearchMake { get; set; }
        public PSFmodel<ModelViewModel> PSFmodels { get; set; }
        public Paging<ModelViewModel>? PaginatedModels { get; set; }
        public SortingHelp SortingModelHelper { get; set; }
        public ModelController
            (
            IVehicleServiceModel _vehicleServiceModel,
            IVehicleServiceMake _vehicleServiceMake
            )
        {
            vehicleServiceModel = _vehicleServiceModel;
            vehicleServiceMake = _vehicleServiceMake;

            PSFmodels = Kernel.Inject<PSFmodel<ModelViewModel>>();
            SortingModelHelper = new SortingHelp();
        }
        public async Task<IActionResult> VehicleModel
            (
            string sortOrderModel,
            string SearchStringModel, string currentFilterModel, int? pageIndexModel
            )
        {
            await UpdatePSFdata
                (
                sortOrderModel, SearchStringModel, currentFilterModel, pageIndexModel
                );
            ViewBag.SortingModelHelper = SortingModelHelper;
            ViewBag.CurrentSearchModel = CurrentSearchModel;
            ViewBag.VehicleMakeIsNull = vehicleServiceMake.VehicleMakeIsNull();                      
            return View(PaginatedModels);
        }

        private async Task UpdatePSFdata
            (string sortOrderModel, string searchStringModel, string currentFilterModel, int? pageIndexModel)
        {
            IQueryable<VehicleModel> SortedFiltered = await PSFmodels.VehicleModelSortFilter
                (sortOrderModel, searchStringModel, currentFilterModel, pageIndexModel);

            IQueryable<ModelViewModel> modelView = SortedFiltered.Select(p=> new ModelViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                Abrv = p.Abrv,
                MakeId = p.MakeId
            });

            PaginatedModels = await PSFmodels.PaginetedModel(modelView, pageIndexModel);
            
            SortingModelHelper = PSFmodels.sortingModel.sortingHelpModel;
            CurrentSearchModel = PSFmodels.filteringModel.CurrentSearchModel;
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
            if (vehicleModel !=null)
            {
                await vehicleServiceModel.Delete(vehicleModel);
            }
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
