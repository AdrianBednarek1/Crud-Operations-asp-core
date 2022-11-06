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
        private List<SelectListItem> VehicleMakeList;
        private IMapper mapper;
        public ModelController(IMapper _mapper)
        {
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<IActionResult> VehicleModel
            (
            string sortOrderModel,
            string searchStringModel, string currentFilterModel, int? pageIndexModel
            )
        {
            List<VehicleModel> vehicleModels = await GetSortFilterPaginateData
                (
                sortOrderModel, searchStringModel, currentFilterModel, pageIndexModel
                );

            List<ModelViewModel> pagedVehicleModel;
            pagedVehicleModel = mapper.Map<List<ModelViewModel>>(vehicleModels);

            Response.StatusCode = StatusCodes.Status200OK;
            return View(pagedVehicleModel);
        }

        private async Task<List<VehicleModel>> GetSortFilterPaginateData
            (string sortOrderModel, string searchStringModel, string currentFilterModel, int? pageIndexModel)
        {
            SortParameters sortParameters = new SortParameters(sortOrderModel);
            FilterParameters filterParameters = new FilterParameters(searchStringModel, currentFilterModel);
            PageParameters pageParameters = new PageParameters(pageIndexModel ?? 1, 4);

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
            List<VehicleMake> listMake = await VehicleServiceMake.GetVehicleMake();
            List<SelectListItem> dropListMake = mapper.Map<List<SelectListItem>>(listMake);

            ViewBag.VehicleMakeList = dropListMake;
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
