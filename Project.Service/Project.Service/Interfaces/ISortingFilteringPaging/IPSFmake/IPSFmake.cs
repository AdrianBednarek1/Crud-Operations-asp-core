using Project.Service.PagingSortingFiltering;
using ZaPrav.NetCore;
using ZaPrav.NetCore.Interfaces;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging.IPSFmake
{
    public interface IPSFmake<T> : IList<T>
    {
        Paging<T>? PaginatedMake { get; }
        ISortingMake sortingMake { get; }
        IFilteringMake filteringMake { get; }
        Task<IQueryable<VehicleMake>> VehicleMakeSortFilter
            (string sortOrderMake, string SearchStringMake, string currentSearchMake, int? pageIndexMake);
        Task<Paging<T>> PaginetedList(IQueryable<T> VehicleQueryable, int? pageIndexMake);
    }
}
