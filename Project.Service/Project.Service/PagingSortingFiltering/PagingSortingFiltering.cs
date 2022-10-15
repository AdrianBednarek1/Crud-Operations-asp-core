using Project.Service.Interfaces.ISortingFilteringPaging;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service
{
    public class PagingSortingFiltering : IPagingSortingFiltering
    {
        private readonly IConfiguration Configuration;
        public PagingSortingFiltering(IConfiguration configuration)
        {
            Configuration = configuration;
            SortingMadeHelper = new SortingHelp();
            SortingModelHelper = new SortingHelp();
        }
        public SortingHelp SortingMadeHelper { get; set; } 
        public SortingHelp SortingModelHelper { get; set; }
        public PaginatedList<VehicleMade>? PaginatedVehicleMades { get; set; }
        public PaginatedList<VehicleModel>? PaginatedVehicleModel { get; set; }
        public async Task<PaginatedList<VehicleMade>> VehicleMadeSFP
            (string sortOrderMades, string SearchStringMade, string currentFilterMade, int? pageIndexMade)
        {
            SortingMadeHelper.CurrentSort = sortOrderMades;
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
            SortingMadeHelper.CurrentFilter = SearchStringMade;

            IQueryable<VehicleMade> vehicleMadesSorting = from b in VehicleService.GetQueryDB().vehicleMades select b;

            if (!String.IsNullOrEmpty(SearchStringMade))
            {
                vehicleMadesSorting = vehicleMadesSorting.Where(s => s.Name.Contains(SearchStringMade) || s.Abrv.Contains(SearchStringMade));
            }
            switch (sortOrderMades)
            {
                case "IdDesc":
                    vehicleMadesSorting = vehicleMadesSorting.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    vehicleMadesSorting = vehicleMadesSorting.OrderBy(s => s.Id);
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

            return PaginatedVehicleMades = await PaginatedList<VehicleMade>.CreateAsync(
                vehicleMadesSorting, pageIndexMade ?? 1, pageSize);
        }
        public async Task<PaginatedList<VehicleModel>> VehicleModelSFP
            (string sortOrderModel, string SearchStringModel, string currentFilterModel, int? pageIndexModel)
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
            SortingMadeHelper.CurrentFilter = SearchStringModel;

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

            return PaginatedVehicleModel = await PaginatedList<VehicleModel>.CreateAsync(
                vehicleModelSorting, pageIndexModel ?? 1, pageSize);
        }
    }
}
