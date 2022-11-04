using Microsoft.AspNetCore.Mvc;
using MVC.project.ViewModels.MakeViewModels;
using ZaPrav.NetCore.VehicleDB;
using AutoMapper;


namespace MVC.project.Controllers
{
    public class MakeController : Controller
    {
        public IMapper mapper { get; set; }
        public MakeController(IMapper _mappe)
        {
            mapper = _mappe;
        }
        [HttpGet]
        public async Task<IActionResult> VehicleMake
            (
            string sortOrderMades,
            string searchStringMade, string currentFilterMade, int? pageIndexMade
            )
        {
            await UpdateSortingFilteringPagingData
                (
                sortOrderMades, searchStringMade, currentFilterMade, pageIndexMade
                );

            List<VehicleMake> vehicleMakesList = await VehicleServiceMake.ReturnMakeList();

            List<MakeViewModel> pagedVehicleMakes;
            pagedVehicleMakes = mapper.Map<List<MakeViewModel>>(vehicleMakesList);

            Response.StatusCode = StatusCodes.Status200OK;
            return View(pagedVehicleMakes);
        }

        private async Task UpdateSortingFilteringPagingData
            (string sortOrderMades, string searchStringMade, string currentFilterMade, int? pageIndexMade)
        {
            await VehicleServiceMake.FilterVehicleMake(searchStringMade,currentFilterMade,pageIndexMade);

            await VehicleServiceMake.PagingVehicleMake(pageIndexMade ?? 1, 4);

            await VehicleServiceMake.SortVehicleMake(sortOrderMades);

            ViewBag.SortingMadeHelper = await VehicleServiceMake.ReturnSortingHelp();
            ViewBag.CurrentSearchMake = await VehicleServiceMake.ReturnCurrentSearch();         
            ViewBag.PagingMake = await VehicleServiceMake.GetPreviousNextPageMake();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await VehicleServiceMake.Delete(id);
            }
            catch (NotSupportedException ex)
            {
                return BadRequest(ex.Message);
            }

            Response.StatusCode = StatusCodes.Status200OK;
            return RedirectToAction("VehicleMake");
        }
        [HttpGet]
        public IActionResult CreateMake()
        {        
            Response.StatusCode= StatusCodes.Status200OK;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateMake(MakeViewModel makeViewModel)
        {
            if (!ModelState.IsValid)
            {          
                Response.StatusCode= StatusCodes.Status422UnprocessableEntity;
                return View();
            }

            VehicleMake vehicleMake = mapper.Map<VehicleMake>(makeViewModel);
            await VehicleServiceMake.Create(vehicleMake);
            
            Response.StatusCode= StatusCodes.Status201Created;
            return RedirectPermanent("VehicleMake");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateMake(int id)
        {
            VehicleMake vehicleMake = await VehicleServiceMake.SearchVehicleMake(id);
            MakeViewModel makeViewModel = mapper.Map<MakeViewModel>(vehicleMake);

            Response.StatusCode= StatusCodes.Status302Found;
            return View(makeViewModel);
        }
        [HttpPost]
        public async Task<IActionResult>UpdateMake(MakeViewModel UpdatedViewModel)
        {
            if (!ModelState.IsValid)
            {
                Response.StatusCode= StatusCodes.Status422UnprocessableEntity;
                return View(UpdatedViewModel);
            }
            VehicleMake UpdatedVehicleMake=mapper.Map<VehicleMake>(UpdatedViewModel);
            await VehicleServiceMake.Update(UpdatedVehicleMake);

            Response.StatusCode= StatusCodes.Status200OK;
            return RedirectToAction("VehicleMake");
        }
    }
}
