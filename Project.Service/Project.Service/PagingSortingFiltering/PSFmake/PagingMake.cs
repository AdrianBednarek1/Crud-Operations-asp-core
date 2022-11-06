using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmake
{
    public class PagingMake
    {
        public int pageIndex { get; private set; }
        public int totalPages { get; private set; }
        public int pageSize { get; private set; }
        public bool hasPreviousPage => pageIndex > 1;
        public bool hasNextPage => pageIndex < totalPages;
        public IQueryable<VehicleMake>? paginetedMakeQuery { get; set; }
        public PagingMake()
        {
            pageIndex = 0;
            totalPages = 0;
        }
        public async Task<IQueryable<VehicleMake>> GetPageMake(PageParameters pageParameters)
        {
            List<VehicleMake> vehicleMake = await VehicleServiceMake.GetVehicleMake();
            IQueryable<VehicleMake> vehicleMakeQuery = vehicleMake.AsQueryable();

            int pageCount = vehicleMakeQuery.Count();

            pageIndex = pageParameters.pageIndex;
            pageSize = pageParameters.pageSize;
            totalPages = (int)Math.Ceiling(pageCount / (double)pageSize);

            paginetedMakeQuery = vehicleMakeQuery.OrderByDescending(d => d.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return paginetedMakeQuery;
        }
    }
}
