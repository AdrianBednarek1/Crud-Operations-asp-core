using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmake
{
    public interface IPagingMake
    {
        int pageIndex { get; }
        int totalPages { get; }
        int pageSize { get; }
        bool hasPreviousPage { get; }
        bool hasNextPage { get; }
        IQueryable<VehicleMake>? paginetedMakeQuery { get; }
        Task<IQueryable<VehicleMake>> GetPageMake(PageParameters pageParameters);
    }
}