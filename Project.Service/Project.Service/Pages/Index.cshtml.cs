using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using ZaPrav.NetCore.VehicleDB;
using Microsoft.Extensions.Configuration;

namespace ZaPrav.NetCore.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration Configuration;

        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public PaginatedList<VehicleMade> PaginatedVehicleMades { get; set; }
        public PaginatedList<VehicleModel> PaginatedVehicleModel { get; set; }
        public  List<VehicleMade> vehicleMades { get; set; }
        public List<VehicleModel> vehicleModels { get; set; }
        public SortingHelp SortingMadeHelper { get; set; } = new SortingHelp();
        public SortingHelp SortingModelHelper { get; set; } = new SortingHelp();
        public string? CurrentFilterMade { get; private set; }
        public string? CurrentFilterModel { get; private set; }
        public string? CurrentSortMade { get; set; }
        public string? CurrentSortModel { get; set; }      
        public async Task OnGetAsync
            (
            string sortOrderMades, 
            string SearchStringMade, string currentFilterMade, int? pageIndexMade,

            string sortOrderModel,
            string SearchStringModel, string currentFilterModel, int? pageIndexModel
            )
        { 
            await RefreshDB();

            await VehicleMadeSortingFilteringPaging(sortOrderMades, SearchStringMade, currentFilterMade, pageIndexMade);
            await VehicleModelSortingFilteringPaging(sortOrderModel, SearchStringModel, currentFilterModel, pageIndexModel);
        }
        private async Task VehicleMadeSortingFilteringPaging(string sortOrderMades, string SearchStringMade, string currentFilterMade, int? pageIndexMade)
        {
            CurrentSortMade = sortOrderMades;
            SortingMadeHelper.NameSort = String.IsNullOrEmpty(sortOrderMades) ? "NameDesc" : "";
            SortingMadeHelper.AbrvSort = sortOrderMades == "Abrv" ? "AbrvDesc" : "Abrv";
            SortingMadeHelper.IdSort = sortOrderMades == "Id" ? "IdDesc" : "Id";
            
            if (SearchStringMade != null)
            {
                pageIndexMade = 1;
            }
            else
            {
                SearchStringMade = currentFilterMade;
            }
            CurrentFilterMade = SearchStringMade;

            IQueryable<VehicleMade> vehicleMadesSorting = from b in VehicleService.GetQueryDB().vehicleMades select b;

            if (!String.IsNullOrEmpty(SearchStringMade))
            {
                vehicleMadesSorting = vehicleMadesSorting.Where(s => s.Name.Contains(SearchStringMade)|| s.Abrv.Contains(SearchStringMade));
            }           
            switch (sortOrderMades)
            {
                case "IdDesc":
                    vehicleMadesSorting = vehicleMadesSorting.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    vehicleMadesSorting= vehicleMadesSorting.OrderBy(s => s.Id);
                    break;
                case "Abrv":
                    vehicleMadesSorting = vehicleMadesSorting.OrderBy(s => s.Abrv);
                    break;
                case "NameDesc":
                    vehicleMadesSorting = vehicleMadesSorting.OrderByDescending(s => s.Name);
                    break;
                case "AbrvDesc":
                    vehicleMadesSorting = vehicleMadesSorting.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    vehicleMadesSorting = vehicleMadesSorting.OrderBy(s => s.Name);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            PaginatedVehicleMades = await PaginatedList<VehicleMade>.CreateAsync(
                vehicleMadesSorting, pageIndexMade ?? 1, pageSize);
        }
        private async Task VehicleModelSortingFilteringPaging(string sortOrderModel, string SearchStringModel, string currentFilterModel, int? pageIndexModel)
        {
            SortingModelHelper.NameSort = String.IsNullOrEmpty(sortOrderModel) ? "NameDesc" : "";
            SortingModelHelper.AbrvSort = sortOrderModel == "Abrv" ? "AbrvDesc" : "Abrv";
            SortingModelHelper.IdSort = sortOrderModel == "Id" ? "IdDesc" : "Id";
            SortingModelHelper.ForeignIdSort = sortOrderModel == "MadeId" ? "MadeIdDesc" : "MadeId";

            if (SearchStringModel != null)
            {
                pageIndexModel = 1;
            }
            else
            {
                SearchStringModel = currentFilterModel;
            }
            CurrentFilterMade = SearchStringModel;

            IQueryable<VehicleModel> vehicleModelSorting = from b in VehicleService.GetQueryDB().vehicleModels select b;

            if (!String.IsNullOrEmpty(SearchStringModel))
            {
                vehicleModelSorting = vehicleModelSorting.Where(s => s.Name.Contains(SearchStringModel) || s.Abrv.Contains(SearchStringModel));
            }
            
            switch (sortOrderModel)
            {
                case "IdDesc":
                    vehicleModelSorting = vehicleModelSorting.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    vehicleModelSorting = vehicleModelSorting.OrderBy(s => s.Id);
                    break;
                case "Abrv":
                    vehicleModelSorting = vehicleModelSorting.OrderBy(s => s.Abrv);
                    break;
                case "NameDesc":
                    vehicleModelSorting = vehicleModelSorting.OrderByDescending(s => s.Name);
                    break;
                case "AbrvDesc":
                    vehicleModelSorting = vehicleModelSorting.OrderByDescending(s => s.Abrv);
                    break;
                case "MadeId":
                    vehicleModelSorting = vehicleModelSorting.OrderBy(s => s.IdMade.Id);
                    break;
                case "MadeIdDesc":
                    vehicleModelSorting = vehicleModelSorting.OrderByDescending(s => s.IdMade.Id);
                    break;
                default:
                    vehicleModelSorting = vehicleModelSorting.OrderBy(s => s.Name);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);
            PaginatedVehicleModel = await PaginatedList<VehicleModel>.CreateAsync(
                vehicleModelSorting, pageIndexModel ?? 1, pageSize);
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
            VehicleModel vehicleModel = vehicleModels.SingleOrDefault(d => d.Id == Id);

            if (vehicleModel != null)
            {
                await VehicleService.Delete(vehicleModel);
            }
        }
        private async Task VehicleMadeDelete(int Id)
        {          
            VehicleMade vehicleMade = vehicleMades.SingleOrDefault(d => d.Id == Id);

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