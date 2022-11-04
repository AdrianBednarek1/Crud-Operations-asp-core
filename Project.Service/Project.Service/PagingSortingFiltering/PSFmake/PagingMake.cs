using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmake
{
    public class PagingMake
    {
        public int pageIndex { get; private set; } 
        public int totalPages { get; private set; } 
        public bool HasPreviousPage => pageIndex > 1;
        public bool HasNextPage => pageIndex < totalPages;
        public IQueryable<VehicleMake>? paginetedMakeQuery;
        public PagingMake()
        {
            pageIndex = 0;
            totalPages = 0;
        }
        public async Task CreateAsync(int _pageIndex, int _pageSize)
        {
            IQueryable<VehicleMake> vehicleMakeQuery = await VehicleServiceMake.GetQueryMake();
            var count = vehicleMakeQuery.Count();

            var paginetedVehicleMake = vehicleMakeQuery.OrderByDescending(d=>d.Id).Skip((_pageIndex - 1) * _pageSize).Take(_pageSize);
            pageIndex = _pageIndex;
            totalPages = (int)Math.Ceiling(count / (double)_pageSize);
            paginetedMakeQuery = paginetedVehicleMake;
        }            
    }
}
