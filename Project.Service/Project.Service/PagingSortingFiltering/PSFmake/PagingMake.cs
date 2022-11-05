using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmake
{
    public class PagingMake
    {
        public int pageIndex { get; private set; } 
        public int totalPages { get; private set; } 
        public bool hasPreviousPage => pageIndex > 1;
        public bool hasNextPage => pageIndex < totalPages;
        public IQueryable<VehicleMake>? paginetedMakeQuery;
        public PagingMake()
        {
            pageIndex = 0;
            totalPages = 0;
        }
        public async Task CreatePagingMake(int _pageIndex, int _pageSize)
        {
            var vehicleMake = await VehicleServiceMake.GetVehicleMake();
            IQueryable<VehicleMake> vehicleMakeQuery = vehicleMake.AsQueryable();

            var count = vehicleMakeQuery.Count();

            paginetedMakeQuery = vehicleMakeQuery.OrderByDescending(d=>d.Id).Skip((_pageIndex - 1) * _pageSize).Take(_pageSize);

            pageIndex = _pageIndex;
            totalPages = (int)Math.Ceiling(count / (double)_pageSize);
        }            
    }
}
