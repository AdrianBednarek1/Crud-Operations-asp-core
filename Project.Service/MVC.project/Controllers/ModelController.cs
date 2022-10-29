using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.project.ViewModels.ModelViewModels;
using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore.VehicleDB;
using Microsoft.EntityFrameworkCore;
using ZaPrav.NetCore;
using Project.Service.PagingSortingFiltering.PSFmodel;
using Project.Service;
using AutoMapper;

namespace MVC.project.Controllers
{
    public class ModelController : Controller
    {
        private IVehicleServiceModel vehicleServiceModel;
        private IVehicleServiceMake vehicleServiceMake;
        private List<SelectListItem> VehicleMakeInList;
        private IMapper mapper;
        public PSFmodel<ModelViewModel> PSFmodels { get; set; }
        public Paging<ModelViewModel>? PaginatedModels { get; set; }
        public ModelController
            (
            IVehicleServiceModel _vehicleServiceModel,
            IVehicleServiceMake _vehicleServiceMake,
            IMapper _mapper
            )
        {
            mapper = _mapper;
            vehicleServiceModel = _vehicleServiceModel;
            vehicleServiceMake = _vehicleServiceMake;

            PSFmodels = Kernel.Inject<PSFmodel<ModelViewModel>>();
        }
        [HttpGet]
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
          
            Response.StatusCode = StatusCodes.Status200OK;
            return View(PaginatedModels);
        }

        private async Task UpdatePSFdata
            (string sortOrderModel, string searchStringModel, string currentFilterModel, int? pageIndexModel)
        {
            IQueryable<VehicleModel> SortedFiltered = await PSFmodels.VehicleModelSortFilter
                (sortOrderModel, searchStringModel, currentFilterModel, pageIndexModel);

            IQueryable<ModelViewModel> modelView = mapper.ProjectTo<ModelViewModel>(SortedFiltered);

            PaginatedModels = await PSFmodels.PaginetedModel(modelView, pageIndexModel);      

            ViewBag.SortingModelHelper = PSFmodels.sortingModel.sortingHelpModel;
            ViewBag.CurrentSearchModel = PSFmodels.filteringModel.CurrentSearchModel;
            ViewBag.VehicleMakeIsNull = vehicleServiceMake.VehicleMakeIsNull();
        }
        [HttpGet]
        public async Task<IActionResult> CreateModel()
        {
            await RefreshDropDown();
            Response.StatusCode= StatusCodes.Status200OK;
            return View();
        }   
        [HttpPost]
        public async Task<IActionResult> CreateModel(ModelViewModel modelViewModel)
        {
            await RefreshDropDown();
            if (!ModelState.IsValid)
            {
                Response.StatusCode= StatusCodes.Status422UnprocessableEntity;
                return View();
            }
            VehicleModel vehicleModel = mapper.Map<VehicleModel>(modelViewModel);
            await vehicleServiceModel.Create(vehicleModel);

            Response.StatusCode= StatusCodes.Status201Created;
            return RedirectToAction("VehicleModel");
        }
        private async Task RefreshDropDown()
        {
            List<VehicleMake> listMake = new List<VehicleMake>();
            listMake = await vehicleServiceMake.GetVehicleMakes();
            VehicleMakeInList = listMake.ConvertAll
            (a =>
                {
                    return new SelectListItem
                    {
                        Text = a.Name,
                        Value = a.Id.ToString()
                    };
                }
            );
            ViewBag.VehicleMakeInList = VehicleMakeInList;
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                VehicleModel vehicleModel = await vehicleServiceModel.SearchVehicleModel(id);
                await vehicleServiceModel.Delete(vehicleModel);        
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(ex.Message);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return RedirectToAction("VehicleModel");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateModel(int id)
        {
            VehicleModel vehicleModel = await vehicleServiceModel.SearchVehicleModel(id);
            await RefreshDropDown();
            ModelViewModel modelViewModel = mapper.Map<ModelViewModel>(vehicleModel);
            
            Response.StatusCode= StatusCodes.Status302Found;
            return View(modelViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateModel(ModelViewModel vehicleModel)
        {
            await RefreshDropDown();
            if (!ModelState.IsValid)
            {
                Response.StatusCode= StatusCodes.Status422UnprocessableEntity;
                return View(vehicleModel);
            }
            VehicleModel model = mapper.Map<VehicleModel>(vehicleModel);
            await vehicleServiceModel.Update(model);
            
            Response.StatusCode= StatusCodes.Status200OK;
            return RedirectToAction("VehicleModel");
        }
    }
}
