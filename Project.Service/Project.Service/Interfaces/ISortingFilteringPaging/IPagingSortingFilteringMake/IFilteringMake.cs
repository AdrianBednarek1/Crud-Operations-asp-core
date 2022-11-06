using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging.IPSFmake
{
    public interface IFilteringMake
    {
        string? currentSearchMake { get; }
        IQueryable<VehicleMake>? filterQueryMake { get; }
        Task<IQueryable<VehicleMake>> GetFilterMake(FilterParameters filterParameters);
    }
}
