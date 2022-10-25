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
using Project.Service.PagingSortingFiltering.PSFmodel;
using Project.Service.PagingSortingFiltering.PSFmake;

namespace ZaPrav.NetCore.Pages
{  
    public class IndexModel : PageModel, IIndexModel
    {
        [BindProperty]
        public int SelectedId { get; set; }
        [BindProperty]
        public List<SelectListItem> VehicleMadesInList { get; set; }
        public string? CurrentSearchModel { get; set; }
        public string? CurrentSearchMake { get; set; }
        public PSFmodel<VehicleModel> PSFmodels { get; set; }
        //public PSFmake PSFmakes { get; set; }
        public Paging<VehicleMake>? PaginatedVehicleMakes { get; set; }
        public Paging<VehicleModel>? PaginatedVehicleModels { get; set; }       
        public SortingHelp SortingMadeHelper { get; set; }
        public SortingHelp SortingModelHelper { get; set; }

        public IVehicleServiceMake vehicleServiceMake;
        public IVehicleServiceModel vehicleServiceModel;
        public IndexModel
            ( 
            IVehicleServiceMake _vehicleServiceMake,
            IVehicleServiceModel _vehicleServiceModel
            )
        {
            PSFmodels = Kernel.Inject<PSFmodel<VehicleModel>>();
            //PSFmakes = Kernel.Inject<PSFmake>();

            vehicleServiceMake = _vehicleServiceMake;
            vehicleServiceModel = _vehicleServiceModel;
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
            //PaginatedVehicleMakes = await PSFmakes.VehicleMakeSFP
            //    (sortOrderMades, SearchStringMade, currentFilterMade, pageIndexMade);

            //PaginatedVehicleModels = await PSFmodels.VehicleModelSortFilter
            //    (sortOrderModel, SearchStringModel, currentFilterModel, pageIndexModel);

            //SortingMadeHelper = PSFmakes.sortingMake.sortingHelpMake;
            SortingModelHelper = PSFmodels.sortingModel.sortingHelpModel;

            CurrentSearchModel = PSFmodels.filteringModel.CurrentSearchModel;
            //CurrentSearchMake = PSFmakes.filteringMake.CurrentSearchMake;
        }
        public async Task<IActionResult> OnPostDeleteAsync(int Id, bool TrueIfModel)
        {
            await RefreshDropDownlist();

            if (!TrueIfModel)
            {
                await VehicleMakeDelete(Id);
            }
            else
            {
                await VehicleModelDelete(Id);
            }
            return RedirectToPage();
        }
        public async Task<IActionResult> OnPostIDAsync()
        {
            var vehicleMades = await vehicleServiceMake.GetVehicleMakes();
            return RedirectToPage("./ModelCreator","ID", vehicleMades.SingleOrDefault(d=>d.Id == SelectedId));
        }
        public async Task<IActionResult> OnGetUpdateVehicleMake(VehicleMake vehicle)
        {
            await vehicleServiceMake.Update(vehicle);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetCreateVehicleMake(VehicleMake vehicle)
        {
            await vehicleServiceMake.Create(vehicle);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetCreateVehicleModel(VehicleModel vehicle)
        {
            await vehicleServiceModel.Create(vehicle);

            return RedirectToPage();
        }
        public async Task<IActionResult> OnGetUpdateVehicleModel(VehicleModel vehicle)
        {
            await vehicleServiceModel.Update(vehicle);

            return RedirectToPage();
        }

        private async Task VehicleModelDelete(int Id)
        {
            VehicleModel? vehicleModel = await vehicleServiceModel.SearchVehicleModel(Id);

            if (vehicleModel != null)
            {
                await vehicleServiceModel.Delete(vehicleModel);
            }
        }
        private async Task VehicleMakeDelete(int Id)
        {          
            VehicleMake? vehicleMade = await vehicleServiceMake.SearchVehicleMake(Id);

            if (vehicleMade != null)
            {
                await vehicleServiceMake.Delete(vehicleMade);
            }
        }
        private async Task RefreshDropDownlist()
        {
            List<VehicleMake> listMake = new List<VehicleMake>();
            listMake  = await vehicleServiceMake.GetVehicleMakes(); 
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