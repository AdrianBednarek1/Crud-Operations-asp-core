using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmodel
{
    public class PSFmodel<T> : List<T>
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
        public Paging<T>? PagingModel { get; set; }
        public SortingModel sortingModel { get; set; }
        public FilteringModel filteringModel { get; set; }
        public async Task<IQueryable<VehicleModel>> VehicleModelSortFilter
            (string sortOrderModel, string SearchStringModel, string currentSearchModel, int? pageIndexModel)
        {
            IQueryable<VehicleModel> VehicleModelQuery = await vehicleServiceModel.GetQueryDBmodel();

            var Filtered = filteringModel.SearchFilterModel
                (SearchStringModel, currentSearchModel,VehicleModelQuery,pageIndexModel);

            var SortedFiltered = sortingModel.SortModel(sortOrderModel, Filtered);

            return SortedFiltered;
        }
        public async Task<Paging<T>> PaginetedModel(IQueryable<T> ModelQuery ,int? pageIndexModel)
        {
            var pageSize = Configuration.GetValue("PageSize", 4);

            return PagingModel = await Paging<T>.CreateAsync
                (ModelQuery, pageIndexModel ?? 1, pageSize);
        }
    }
}
