using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.project.ViewModels.ModelViewModels;
using ZaPrav.NetCore.VehicleDB;
using AutoMapper;
using Project.Service.VehicleService;
using Project.Service.PagingSortingFiltering.Parameters;

namespace MVC.project.Controllers
{
    public class ModelController : Controller
    {
        private IMapper mapper;
        public ModelController(IMapper _mapper)
        {
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<IActionResult> VehicleModel
            (string sortOrderModel, string searchStringModel, string currentFilterModel, int? pageIndexModel)
        {
            List<VehicleModel> vehicleModels = await GetModelList(sortOrderModel, searchStringModel, currentFilterModel, pageIndexModel);
            List<ModelViewModel> modelViewModel = mapper.Map<List<ModelViewModel>>(vehicleModels);

            Response.StatusCode = StatusCodes.Status200OK;
            return View(modelViewModel);
        }
        private async Task<List<VehicleModel>> GetModelList
            (string sortOrderModel, string searchStringModel, string currentFilterModel, int? pageIndexModel)
        {
            int pageSize = 4;
            SortParameters sortParameters = new SortParameters(sortOrderModel);
            FilterParameters filterParameters = new FilterParameters(searchStringModel, currentFilterModel);
            PageParameters pageParameters = new PageParameters(pageIndexModel, pageSize);

            List<VehicleModel> vehicleModel = await VehicleServiceModel.GetVehicleModel(sortParameters, filterParameters, pageParameters);

            ViewBag.SortingModelHelper = await VehicleServiceModel.ReturnSortingHelp();
            ViewBag.CurrentSearchModel = await VehicleServiceModel.ReturnCurrentSearch();
            ViewBag.pagingModel = await VehicleServiceModel.GetPreviousNextPageModel();

            List<VehicleMake> vehicleMakeList = await VehicleServiceMake.GetVehicleMake();
            ViewBag.VehicleMakeIsNull = vehicleMakeList.Any() ? false : true;

            return vehicleModel;
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
            List<VehicleMake> vehicleMake = await VehicleServiceMake.GetVehicleMake();
            List<SelectListItem> selecetListMake = mapper.Map<List<SelectListItem>>(vehicleMake);

            ViewBag.VehicleMakeList = selecetListMake;
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            VehicleModel deleteVehicleModel = await VehicleServiceModel.GetModelById(id);
            await VehicleServiceModel.Delete(deleteVehicleModel);

            Response.StatusCode = StatusCodes.Status200OK;
            return RedirectToAction("VehicleModel");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateModel(int id)
        {
            await RefreshDropDown();
            VehicleModel vehicleModel = await VehicleServiceModel.GetModelById(id);
            ModelViewModel modelViewModel = mapper.Map<ModelViewModel>(vehicleModel);

            Response.StatusCode = StatusCodes.Status302Found;
            return View(modelViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateModel(ModelViewModel modelViewModel)
        {
            await RefreshDropDown();
            if (!ModelState.IsValid)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return View(modelViewModel);
            }
            VehicleModel updateModel = mapper.Map<VehicleModel>(modelViewModel);
            await VehicleServiceModel.Update(updateModel);

            Response.StatusCode = StatusCodes.Status200OK;
            return RedirectToAction("VehicleModel");
        }
    }
}
