using Microsoft.AspNetCore.Mvc;
using MVC.project.ViewModels.MakeViewModels;
using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore.VehicleDB;
using Microsoft.EntityFrameworkCore;
using ZaPrav.NetCore;
using Project.Service.PagingSortingFiltering.PSFmake;
using Project.Service;

namespace MVC.project.Controllers
{
    public class MakeController : Controller
    {
        private IVehicleServiceMake vehicleServiceMake;
        public string? CurrentSearchMake { get; set; }
        public PSFmake PSFmakes { get; set; }
        public Paging<VehicleMake>? PaginatedVehicleMakes { get; set; }
        public SortingHelp SortingMadeHelper { get; set; }
        public MakeController(IVehicleServiceMake _vehicleServiceMake)
        {
            PSFmakes = Kernel.Inject<PSFmake>();
            vehicleServiceMake = _vehicleServiceMake;

            vehicleServiceMake = _vehicleServiceMake;
            SortingMadeHelper = new SortingHelp();
        }
        public async Task<IActionResult> VehicleMake
            (
            string sortOrderMades,
            string SearchStringMade, string currentFilterMade, int? pageIndexMade
            )
        {

            await UpdatePSF
                (
                sortOrderMades, SearchStringMade, currentFilterMade, pageIndexMade
                );

            ViewBag.SortingMadeHelper = SortingMadeHelper;
            ViewBag.CurrentSearchMake = CurrentSearchMake;

            return View(PaginatedVehicleMakes);
        }

        private async Task UpdatePSF
            (
            string sortOrderMades, string searchStringMade, string currentFilterMade, int? pageIndexMade
            )
        {
            PaginatedVehicleMakes = await PSFmakes.VehicleMakeSFP
                (sortOrderMades, searchStringMade, currentFilterMade, pageIndexMade);

            SortingMadeHelper = PSFmakes.sortingMake.sortingHelpMake;
            CurrentSearchMake = PSFmakes.filterMake.CurrentSearchMake;
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
        public async Task<IActionResult> CreateMake(CreateMakeViewModel makeViewModel)
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
