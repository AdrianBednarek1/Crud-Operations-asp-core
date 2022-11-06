using Microsoft.AspNetCore.Mvc;
using MVC.project.ViewModels.MakeViewModels;
using ZaPrav.NetCore.VehicleDB;
using AutoMapper;
using Project.Service.PagingSortingFiltering.Parameters;

namespace MVC.project.Controllers
{
    public class MakeController : Controller
    {
        private IMapper mapper;
        public MakeController(IMapper _mappe)
        {
            mapper = _mappe;
        }
        [HttpGet]
        public async Task<IActionResult> VehicleMake
            (string sortOrderMades, string searchStringMade, string currentFilterMade, int? pageIndexMade)
        {
            List<VehicleMake> makeList = await GetMakeList(sortOrderMades, searchStringMade, currentFilterMade, pageIndexMade);
            List<MakeViewModel> makeViewModels = mapper.Map<List<MakeViewModel>>(makeList);

            Response.StatusCode = StatusCodes.Status200OK;
            return View(makeViewModels);
        }
        private async Task<List<VehicleMake>> GetMakeList
            (string sortOrderMake, string searchStringMake, string currentFilterMake, int? pageIndexMake)
        {
            int pageSize = 5;
            FilterParameters filterParameters = new FilterParameters(searchStringMake, currentFilterMake);
            PageParameters pageParameters = new PageParameters(pageIndexMake, pageSize);
            SortParameters sortParameters = new SortParameters(sortOrderMake);

            List<VehicleMake> sortFilterPage = await VehicleServiceMake.GetVehicleMake(pageParameters, filterParameters, sortParameters);

            ViewBag.SortingMadeHelper = await VehicleServiceMake.ReturnSortingHelp();
            ViewBag.CurrentSearchMake = await VehicleServiceMake.ReturnCurrentSearch();
            ViewBag.PagingMake = await VehicleServiceMake.GetPreviousNextPageMake();

            return sortFilterPage;
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await VehicleServiceMake.Delete(id);

            Response.StatusCode = StatusCodes.Status200OK;
            return RedirectToAction("VehicleMake");
        }
        [HttpGet]
        public IActionResult CreateMake()
        {
            Response.StatusCode = StatusCodes.Status200OK;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMake(MakeViewModel makeViewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return View();
            }

            VehicleMake createVehicleMake = mapper.Map<VehicleMake>(makeViewModel);
            await VehicleServiceMake.Create(createVehicleMake);

            Response.StatusCode = StatusCodes.Status201Created;
            return RedirectPermanent("VehicleMake");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateMake(int id)
        {
            VehicleMake vehicleMake = await VehicleServiceMake.GetByIdMake(id);
            MakeViewModel makeViewModel = mapper.Map<MakeViewModel>(vehicleMake);

            Response.StatusCode = StatusCodes.Status302Found;
            return View(makeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateMake(MakeViewModel MakeViewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                return View(MakeViewModel);
            }
            VehicleMake updateVehicleMake = mapper.Map<VehicleMake>(MakeViewModel);
            await VehicleServiceMake.Update(updateVehicleMake);

            Response.StatusCode = StatusCodes.Status200OK;
            return RedirectToAction("VehicleMake");
        }
    }
}
