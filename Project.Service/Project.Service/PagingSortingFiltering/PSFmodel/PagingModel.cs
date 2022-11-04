using Project.Service.VehicleService;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmodel
{
    public class PagingModel
    {
        public int pageIndex { get; private set; }
        public int totalPages { get; private set; }
        public bool hasPreviousPage => pageIndex > 1;
        public bool hasNextPage => pageIndex < totalPages;
        public IQueryable<VehicleModel>? paginetedModelQuery;
        public PagingModel()
        {
            pageIndex = 0;
            totalPages = 0;
        }
        public async Task CreateAsync(int _pageIndex, int _pageSize)
        {
            IQueryable<VehicleModel> vehicleModelQuery = await VehicleServiceModel.GetQueryDBmodel();
            var count = vehicleModelQuery.Count();

            var paginetedVehicleModel = vehicleModelQuery.OrderByDescending(d => d.Id).Skip((_pageIndex - 1) * _pageSize).Take(_pageSize);
            pageIndex = _pageIndex;
            totalPages = (int)Math.Ceiling(count / (double)_pageSize);
            paginetedModelQuery = paginetedVehicleModel;
        }
    }
}
