using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmodel
{
    public interface IPagingModel
    {
        int pageIndex { get; }
        int totalPages { get; }
        bool hasPreviousPage { get; }
        bool hasNextPage { get; }
        IQueryable<VehicleModel>? paginetedModelQuery { get; }
        Task CreateAsync(int _pageIndex, int _pageSize);
    }
}