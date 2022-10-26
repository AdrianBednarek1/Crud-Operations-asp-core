using Project.Service.Interfaces.IVehicleRepository;
using System.Linq;
using System.Reflection;
using ZaPrav.NetCore;
using ZaPrav.NetCore.Interfaces;
using ZaPrav.NetCore.VehicleDB;
using Microsoft.EntityFrameworkCore;
using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmake;

namespace Project.Service.PagingSortingFiltering.PSFmake
{
    public class PSFmake<T> : List<T>, IPSFmake<T>
    {
        private readonly IConfiguration Configuration;
        private IVehicleServiceMake vehicleServiceMake;
        public PSFmake
            (
            IConfiguration configuration, IVehicleServiceMake _vehicleServiceMake, 
            IFilteringMake _filteringMake, ISortingMake _sortingMake
            )
        {
            vehicleServiceMake = _vehicleServiceMake;
            Configuration = configuration;
            filteringMake = _filteringMake;
            sortingMake = _sortingMake;
        }
        public Paging<T>? PaginatedMake { get; set; }
        public ISortingMake sortingMake { get; set; }
        public IFilteringMake filteringMake { get; set; }
        public async Task<IQueryable<VehicleMake>> VehicleMakeSortFilter
            (string sortOrderMake, string SearchStringMake, string currentSearchMake, int? pageIndexMake) 
        {
            IQueryable<VehicleMake> VehicleMakeQuery = await vehicleServiceMake.GetQueryDBmake();

            var Filtered = filteringMake.SearchFilterMake
                (SearchStringMake, currentSearchMake, VehicleMakeQuery, pageIndexMake);

            var FilteredSorted = sortingMake.SortMake(sortOrderMake, Filtered);

            return FilteredSorted;
        }
        public async Task<Paging<T>> PaginetedList(IQueryable<T> VehicleQueryable, int? pageIndexMake) 
        {
            var pageSize = Configuration.GetValue("PageSize", 4);

            return PaginatedMake = await Paging<T>.CreateAsync
                (VehicleQueryable, pageIndexMake ?? 1, pageSize);
        }
    }
}

