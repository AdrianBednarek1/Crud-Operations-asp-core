using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmake
{
    public class PSFmake
    {
        private readonly IConfiguration Configuration;
        private IVehicleServiceMake vehicleServiceMake;
        public PSFmake(IConfiguration configuration, IVehicleServiceMake _vehicleServiceMake)
        {
            vehicleServiceMake = _vehicleServiceMake;
            Configuration = configuration;
            filterMake = new FilterMake();
            sortingMake = new SortingMake();
        }
        public Paging<VehicleMake>? PaginatedVehicleModel { get; set; }
        public SortingMake sortingMake { get; set; }
        public FilterMake filterMake { get; set; }
        public async Task<Paging<VehicleMake>> VehicleMakeSFP
            (string sortOrderMake, string SearchStringMake, string currentSearchMake, int? pageIndexMake)
        {
            IQueryable<VehicleMake> VehicleMakeQuery = vehicleServiceMake.GetQueryDBmake();

            VehicleMakeQuery = filterMake.SearchFilterMake
                (SearchStringMake, currentSearchMake, VehicleMakeQuery, pageIndexMake);

            VehicleMakeQuery = sortingMake.SortMake(sortOrderMake, VehicleMakeQuery);

            var pageSize = Configuration.GetValue("PageSize", 4);

            return PaginatedVehicleModel = await Paging<VehicleMake>.CreateAsync(
                VehicleMakeQuery, pageIndexMake ?? 1, pageSize);
        }
    }
}
