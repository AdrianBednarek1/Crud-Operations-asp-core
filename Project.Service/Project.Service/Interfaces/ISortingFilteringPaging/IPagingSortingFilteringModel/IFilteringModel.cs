using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel
{
    public interface IFilteringModel
    {
        public string? CurrentSearchModel { get; }

        public IQueryable<VehicleModel> SearchFilterModel
            (string SearchString, string CurrentSearch, IQueryable<VehicleModel> vehicleModelSorting, int? pageIndexMade);
    }
}
