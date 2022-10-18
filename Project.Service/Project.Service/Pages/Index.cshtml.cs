using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZaPrav.NetCore.VehicleDB;
using ZaPrav.NetCore.Interfaces.IPages.IIndex;
using Project.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service.Interfaces.ISortingFilteringPaging;
using AutoMapper;
using Project.Service.Interfaces.IVehicleRepository;
using Ninject;

namespace ZaPrav.NetCore.Pages
{  
    public class IndexModel : PageModel, IIndexModel
    {
        [BindProperty]
        public int SelectedId { get; set; }
        [BindProperty]
        public List<SelectListItem> VehicleMadesInList { get; set; }

        public PagingSortingFiltering SFP { get; set; }
        public PaginatedList<VehicleMake>? PaginatedVehicleMakes { get; set; }
        public PaginatedList<VehicleModel>? PaginatedVehicleModels { get; set; }       
        public SortingHelp SortingMadeHelper { get; set; }
        public SortingHelp SortingModelHelper { get; set; }

        public IVehicleService vehicleService;
        public IndexModel( IVehicleService _vehicleService)
        {
            SFP = Kernel.Inject<PagingSortingFiltering>();
            vehicleService = _vehicleService;
            SortingMadeHelper = new SortingHelp();
            SortingModelHelper = new SortingHelp();
        }
        public async Task OnGetAsync
            (
            string sortOrderMades,
            string SearchStringMade, string currentFilterMade, int? pageIndexMade,

            string sortOrderModel,
            string SearchStringModel, string currentFilterModel, int? pageIndexModel
            )
        {
            await RefreshDropDownlist();

            await GetUpdateSFPdata
                (
                sortOrderMades, SearchStringMade, currentFilterMade, pageIndexMade,

                sortOrderModel, SearchStringModel, currentFilterModel, pageIndexModel
                );
        }
        private async Task GetUpdateSFPdata
            (
            string sortOrderMades,
            string SearchStringMade, string currentFilterMade, int? pageIndexMade,

            string sortOrderModel,
            string SearchStringModel, string currentFilterModel, int? pageIndexModel
            )
        {
            PaginatedVehicleMakes = await SFP.VehicleMadeSFP
                (sortOrderMades, SearchStringMade, currentFilterMade, pageIndexMade);

            PaginatedVehicleModels = await SFP.VehicleModelSFP
                (sortOrderModel, SearchStringModel, currentFilterModel, pageIndexModel);

            SortingMadeHelper = SFP.SortingMakeHelper;
            SortingModelHelper = SFP.SortingModelHelper;
        }
        public async Task<IActionResult> OnPostDeleteAsync(int Id, bool TrueIfModel)
        {
            await RefreshDropDownlist();

            if (!TrueIfModel)
            {
                await VehicleMadeDelete(Id);
            }
            else
            {
                await VehicleModelDelete(Id);
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostIDAsync()
        {
            var vehicleMades = await vehicleService.GetVehicleMakes();
            return RedirectToPage("./ModelCreator","ID", vehicleMades.SingleOrDefault(d=>d.Id == SelectedId));
        }
        public async Task<IActionResult> OnGetUpdateVehicleMake(VehicleMake vehicle)
        {
            await vehicleService.Update(vehicle);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetCreateVehicleMake(VehicleMake vehicle)
        {
            await vehicleService.Create(vehicle);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetCreateVehicleModel(VehicleModel vehicle)
        {
            await vehicleService.Create(vehicle);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetUpdateVehicleModel(VehicleModel vehicle)
        {
            await vehicleService.Update(vehicle);

            return RedirectToPage();
        }

        private async Task VehicleModelDelete(int Id)
        {
            VehicleModel? vehicleModel = await vehicleService.SearchVehicleModel(Id);

            if (vehicleModel != null)
            {
                await vehicleService.Delete(vehicleModel);
            }
        }
        private async Task VehicleMadeDelete(int Id)
        {          
            VehicleMake? vehicleMade = await vehicleService.SearchVehicleMake(Id);

            if (vehicleMade != null)
            {
                await vehicleService.Delete(vehicleMade);
            }
        }
        private async Task RefreshDropDownlist()
        {
            List<VehicleMake> listMake = new List<VehicleMake>();
            listMake  = await vehicleService.GetVehicleMakes(); 
            VehicleMadesInList = listMake.ConvertAll(a =>
            {
                return new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                };
            });
        }

    }
}