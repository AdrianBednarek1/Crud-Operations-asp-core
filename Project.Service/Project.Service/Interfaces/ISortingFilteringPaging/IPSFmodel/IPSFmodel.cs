using Project.Service.PagingSortingFiltering;
using Project.Service.PagingSortingFiltering.PSFmodel;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel
{
    public interface IPSFmodel<T> : IList<T> 
    {
        Paging<T>? PagingModel { get; }
        ISortingModel sortingModel { get; }
        IFilteringModel filteringModel { get; }
        Task<IQueryable<VehicleModel>> VehicleModelSortFilter
            (string sortOrderModel, string SearchStringModel, string currentSearchModel, int? pageIndexModel);
        Task<Paging<T>> PaginetedModel(IQueryable<T> ModelQuery, int? pageIndexModel);
    }
}
