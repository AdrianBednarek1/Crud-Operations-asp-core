using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmodel
{
    public class PSFmodel
    {
        private readonly IConfiguration Configuration;
        private IVehicleServiceModel vehicleServiceModel;
        public PSFmodel(IConfiguration configuration, IVehicleServiceModel _vehicleServiceModel)
        {
            vehicleServiceModel = _vehicleServiceModel;
            Configuration = configuration;
            filteringModel = new FilteringModel();
            sortingModel = new SortingModel();
        }
        public Paging<VehicleModel>? PaginatedVehicleModel { get; set; }
        public SortingModel sortingModel { get; set; }
        public FilteringModel filteringModel { get; set; }
        public async Task<Paging<VehicleModel>> VehicleModelSFP
            (string sortOrderModel, string SearchStringModel, string currentSearchModel, int? pageIndexModel)
        {
            IQueryable<VehicleModel> VehicleModelQuery = from b in vehicleServiceModel.GetQueryDBmodel() select b;

            VehicleModelQuery = filteringModel.SearchFilterModel
                (SearchStringModel, currentSearchModel,VehicleModelQuery,pageIndexModel);

            VehicleModelQuery = sortingModel.SortModel(sortOrderModel, VehicleModelQuery);

            var pageSize = Configuration.GetValue("PageSize", 4);

            return PaginatedVehicleModel = await Paging<VehicleModel>.CreateAsync(
                VehicleModelQuery, pageIndexModel ?? 1, pageSize);
        }
    }
}
