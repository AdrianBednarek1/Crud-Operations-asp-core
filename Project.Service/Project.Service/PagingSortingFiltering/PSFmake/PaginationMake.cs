using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmake
{
    public class PaginationMake
    {
        public int limit { get; set; }
        public int start { get; set; }
        public int totalItems { get; set; }
        public float numberPage { get; set; }
        public PaginationMake()
        {
            limit = 3;
        }
        public List<VehicleMake> Pagination(int page,IQueryable<VehicleMake> vehicleMakes)
        {
            totalItems = vehicleMakes.Count();
            start = page - 1 * limit;
            numberPage = totalItems/ limit;
            numberPage = (int)Math.Ceiling(numberPage);

            return  vehicleMakes.Skip(start).Take(limit).ToList();
        }
    }
}
