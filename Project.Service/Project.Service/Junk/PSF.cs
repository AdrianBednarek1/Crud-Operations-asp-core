using Project.Service.Interfaces.ISortingFilteringPaging;
using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service
{
    public class PSF : IPagingSortingFiltering
    {
        private readonly IConfiguration Configuration;
        
        private IVehicleServiceMake vehicleServiceMake;
        private IVehicleServiceModel vehicleServiceModel;
        public PSF(IConfiguration configuration, IVehicleServiceMake _vehicleService, IVehicleServiceModel _vehicleServiceModel)
        {
            vehicleServiceMake = _vehicleService;
            Configuration = configuration;
            vehicleServiceModel = _vehicleServiceModel;
            SortingMakeHelper = new SortingHelp();
            SortingModelHelper = new SortingHelp();
        }
        public SortingHelp SortingMakeHelper { get; set; } 
        public SortingHelp SortingModelHelper { get; set; }
        public Paging<VehicleMake>? PaginatedVehicleMades { get; set; }
        public Paging<VehicleModel>? PaginatedVehicleModel { get; set; }
        public async Task<Paging<VehicleMake>> VehicleMadeSFP
            (string sortOrderMades, string SearchStringMade, string currentFilterMade, int? pageIndexMade)
        {
            SortingMakeHelper.CurrentSort = sortOrderMades;
            SortingMakeHelper.NameSort = String.IsNullOrEmpty(sortOrderMades) ? "NameDesc" : "";
            SortingMakeHelper.AbrvSort = sortOrderMades == "Abrv" ? "AbrvDesc" : "Abrv";
            SortingMakeHelper.IdSort = sortOrderMades == "Id" ? "IdDesc" : "Id";

            if (SearchStringMade != null)
            {
                pageIndexMade = 1;
            }
            else
            {
                SearchStringMade = currentFilterMade;
            }
            //SortingMakeHelper.CurrentFilter = SearchStringMade;

            IQueryable<VehicleMake> vehicleMadesSorting = from b in vehicleServiceMake.GetQueryDBmake() select b;

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

            return PaginatedVehicleMades = await Paging<VehicleMake>.CreateAsync(
                vehicleMadesSorting, pageIndexMade ?? 1, pageSize);
        }
        public async Task<Paging<VehicleModel>> VehicleModelSFP
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
            //SortingModelHelper.CurrentFilter = SearchStringModel;

            IQueryable<VehicleModel> vehicleModelSorting = from b in vehicleServiceModel.GetQueryDBmodel() select b;

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
                    vehicleModelSorting = vehicleModelSorting.OrderBy(s => s.MakeId);
                    break;
                case "MadeIdDesc":
                    vehicleModelSorting = vehicleModelSorting.OrderByDescending(s => s.MakeId);
                    break;
                default:
                    vehicleModelSorting = vehicleModelSorting.OrderBy(s => s.Name);
                    break;
            }
            var pageSize = Configuration.GetValue("PageSize", 4);

            return PaginatedVehicleModel = await Paging<VehicleModel>.CreateAsync(
                vehicleModelSorting, pageIndexModel ?? 1, pageSize);
        }
    }
}
