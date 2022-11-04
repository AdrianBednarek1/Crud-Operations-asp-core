using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging.IPSFmake
{
    public interface IFilteringMake
    {
        public string? CurrentSearchMake { get; }
        public IQueryable<VehicleMake> SearchFilterMake
            (string SearchString, string CurrentSearch, IQueryable<VehicleMake> vehicleMakes, int? pageIndexMade);
    }
}
