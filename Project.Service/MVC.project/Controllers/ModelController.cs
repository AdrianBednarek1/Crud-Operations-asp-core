using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.project.ViewModels.ModelViewModels;
using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore.VehicleDB;
using ZaPrav.NetCore;
using Project.Service.PagingSortingFiltering.PSFmodel;
using Project.Service;
using AutoMapper;
using Project.Service.VehicleService;

namespace MVC.project.Controllers
{
    public class ModelController : Controller
    {
        private List<SelectListItem> VehicleMakeInList;
        private IMapper mapper;
        public ModelController(IMapper _mapper)
        {
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<IActionResult> VehicleModel
            (
            string sortOrderModel,
            string SearchStringModel, string currentFilterModel, int? pageIndexModel
            )
        {
            await PagingSortingFilteringData
                (
                sortOrderModel, SearchStringModel, currentFilterModel, pageIndexModel
                );
            List<VehicleModel> vehicleModelList = await VehicleServiceModel.PaginatedFilteredSortedModelList();

            List<ModelViewModel> pagedVehicleModel;
            pagedVehicleModel = mapper.Map<List<ModelViewModel>>(vehicleModelList);

            Response.StatusCode = StatusCodes.Status200OK;
            return View(pagedVehicleModel);
        }

        private async Task PagingSortingFilteringData
            (string sortOrderModel, string searchStringModel, string currentFilterModel, int? pageIndexModel)
        {
            await VehicleServiceModel.FilterVehicleModel(searchStringModel, currentFilterModel);

            await VehicleServiceModel.PagingVehicleModel(pageIndexModel ?? 1, 4);

            await VehicleServiceModel.SortVehicleModel(sortOrderModel);

            ViewBag.SortingModelHelper = await VehicleServiceModel.ReturnSortingHelp();
            ViewBag.CurrentSearchModel = await VehicleServiceModel.ReturnCurrentSearch();
            ViewBag.pagingModel = await VehicleServiceModel.GetPreviousNextPageModel();
            ViewBag.VehicleMakeIsNull = await VehicleServiceModel.VehicleMakeIsNull();
        }
        [HttpGet]
        public async Task<IActionResult> CreateModel()
        {
            await RefreshDropDown();
            Response.StatusCode = StatusCodes.Status200OK;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateModel(ModelViewModel modelViewModel)
        {
            await RefreshDropDown();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return View();
            }
            VehicleModel vehicleModel = mapper.Map<VehicleModel>(modelViewModel);
            await VehicleServiceModel.Create(vehicleModel);

            Response.StatusCode = StatusCodes.Status201Created;
            return RedirectToAction("VehicleModel");
        }
        private async Task RefreshDropDown()
        {
            var vehicleMake = await VehicleServiceMake.GetVehicleMake();
            List<VehicleMake> listMake = vehicleMake.ToList();

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
                VehicleModel vehicleModel = await VehicleServiceModel.GetModelById(id);
                await VehicleServiceModel.Delete(vehicleModel);
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
            VehicleModel vehicleModel = await VehicleServiceModel.GetModelById(id);
            await RefreshDropDown();
            ModelViewModel modelViewModel = mapper.Map<ModelViewModel>(vehicleModel);

            Response.StatusCode = StatusCodes.Status302Found;
            return View(modelViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateModel(ModelViewModel vehicleModel)
        {
            await RefreshDropDown();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return View(vehicleModel);
            }
            VehicleModel model = mapper.Map<VehicleModel>(vehicleModel);
            await VehicleServiceModel.Update(model);

            Response.StatusCode = StatusCodes.Status200OK;
            return RedirectToAction("VehicleModel");
        }
    }
}
