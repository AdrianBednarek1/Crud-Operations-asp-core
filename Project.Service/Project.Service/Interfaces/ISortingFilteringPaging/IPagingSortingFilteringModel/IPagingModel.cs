using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmodel
{
    public interface IPagingModel
    {
        public int pageIndex { get; }
        int totalPages { get; }
        int pageSize { get; }
        bool hasPreviousPage => pageIndex > 1;
        bool hasNextPage => pageIndex < totalPages;
        IQueryable<VehicleModel>? paginetedModelQuery { get; }
        Task<IQueryable<VehicleModel>> GetPageModel(PageParameters pageParameters);
    }
}