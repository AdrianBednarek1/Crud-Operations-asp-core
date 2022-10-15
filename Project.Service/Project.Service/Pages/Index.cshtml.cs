using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZaPrav.NetCore.VehicleDB;
using ZaPrav.NetCore.Interfaces.IPages.IIndex;
using Ninject;
using Project.Service;

namespace ZaPrav.NetCore.Pages
{
    public class IndexModel :  PageModel, IIndexModel
    {
        private readonly IConfiguration Configuration;
        public PagingSortingFiltering SFP { get; private set; }     
        public PaginatedList<VehicleMake>? PaginatedVehicleMades { get; set; }
        public PaginatedList<VehicleModel>? PaginatedVehicleModel { get; set; }
        public  List<VehicleMake> vehicleMades { get; set; }
        public List<VehicleModel> vehicleModels { get; set; }
        public SortingHelp SortingMadeHelper { get; set; } 
        public SortingHelp SortingModelHelper { get; set; } 
        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
            SFP = new PagingSortingFiltering(Configuration);

            vehicleMades = new List<VehicleMake>();
            vehicleModels = new List<VehicleModel>();
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
            await RefreshDB();

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
            PaginatedVehicleMades = await SFP.VehicleMadeSFP
                (sortOrderMades, SearchStringMade, currentFilterMade, pageIndexMade);

            PaginatedVehicleModel = await SFP.VehicleModelSFP
                (sortOrderModel, SearchStringModel, currentFilterModel, pageIndexModel);
            
            SortingMadeHelper = SFP.SortingMadeHelper;
            SortingModelHelper = SFP.SortingModelHelper;
        }
        public async Task<IActionResult> OnPostDeleteAsync(int Id, bool TrueIfModel)
        {          
            await RefreshDB();

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
        private async Task VehicleModelDelete(int Id)
        {
            VehicleModel? vehicleModel = vehicleModels.SingleOrDefault(d => d.Id == Id);

            if (vehicleModel != null)
            {
                await VehicleService.Delete(vehicleModel);
            }
        }
        private async Task VehicleMadeDelete(int Id)
        {          
            VehicleMake? vehicleMade = vehicleMades.SingleOrDefault(d => d.Id == Id);

            if (vehicleMade != null)
            {
                await VehicleService.Delete(vehicleMade);
            }
        }
        private async Task RefreshDB()
        {
            vehicleMades = await VehicleService.GetVehicleMades();
            vehicleModels = await VehicleService.GetVehicleModels();
        }
    }
}