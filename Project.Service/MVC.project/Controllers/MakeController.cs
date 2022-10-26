using Microsoft.AspNetCore.Mvc;
using MVC.project.ViewModels.MakeViewModels;
using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore.VehicleDB;
using ZaPrav.NetCore;
using Project.Service.PagingSortingFiltering.PSFmake;
using Project.Service;
using AutoMapper;


namespace MVC.project.Controllers
{
    public class MakeController : Controller
    {
        private IVehicleServiceMake vehicleServiceMake;
        public string? CurrentSearchMake { get; set; }
        public PSFmake<MakeViewModel> PSFmakes { get; set; }
        public Paging<MakeViewModel>? PaginatedMakes{ get; set; }
        public SortingHelp SortingMadeHelper { get; set; }
        public IMapper mapper { get; set; }
        public MakeController(IVehicleServiceMake _vehicleServiceMake, IMapper _mappe)
        {
            PSFmakes = Kernel.Inject<PSFmake<MakeViewModel>>();
            vehicleServiceMake = _vehicleServiceMake;
            mapper = _mappe;
            SortingMadeHelper = new SortingHelp();
        }
        [HttpGet]
        public async Task<IActionResult> VehicleMake
            (
            string sortOrderMades,
            string SearchStringMade, string currentFilterMade, int? pageIndexMade
            )
        {
            await UpdatePSFdata
                (
                sortOrderMades, SearchStringMade, currentFilterMade, pageIndexMade
                );

            ViewBag.SortingMadeHelper = SortingMadeHelper;
            ViewBag.CurrentSearchMake = CurrentSearchMake;

            Response.StatusCode= StatusCodes.Status200OK;
            return View(PaginatedMakes);
        }

        private async Task UpdatePSFdata
            (string sortOrderMades, string searchStringMade, string currentFilterMade, int? pageIndexMade)
        {
            IQueryable<VehicleMake> SortedFiltered = await PSFmakes.VehicleMakeSortFilter
                (sortOrderMades, searchStringMade, currentFilterMade, pageIndexMade);

            IQueryable<MakeViewModel> makeViewModels = mapper.ProjectTo<MakeViewModel>(SortedFiltered);

            PaginatedMakes = await PSFmakes.PaginetedList(makeViewModels, pageIndexMade);

            SortingMadeHelper = PSFmakes.sortingMake.sortingHelpMake;

            CurrentSearchMake = PSFmakes.filteringMake.CurrentSearchMake;
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                VehicleMake vehicleMake = await vehicleServiceMake.SearchVehicleMake(id);
                await vehicleServiceMake.Delete(vehicleMake);
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
            await vehicleServiceMake.Create(vehicleMake);
            
            Response.StatusCode= StatusCodes.Status201Created;
            return RedirectToAction("VehicleMake");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateMake(int id)
        {
            VehicleMake vehicleMake = await vehicleServiceMake.SearchVehicleMake(id);
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
            await vehicleServiceMake.Update(UpdatedVehicleMake);

            Response.StatusCode= StatusCodes.Status200OK;
            return RedirectToAction("VehicleMake");
        }
    }
}
