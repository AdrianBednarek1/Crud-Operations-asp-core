using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.VehicleService;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmodel
{
    public class PagingModel // : IPagingModel
    {
        public int pageIndex { get; private set; }
        public int totalPages { get; private set; }
        public int pageSize { get; private set; }
        public bool hasPreviousPage => pageIndex > 1;
        public bool hasNextPage => pageIndex < totalPages;
        public IQueryable<VehicleModel>? paginetedModelQuery { get; set; }
        public PagingModel()
        {
            pageSize = 0;
            pageIndex = 0;
            totalPages = 0;
        }
        public async Task<IQueryable<VehicleModel>> GetPageModel(PageParameters pageParameters)
        {
            List<VehicleModel> vehicleModel = await VehicleServiceModel.GetVehicleModel();
            IQueryable<VehicleModel> vehicleModelQuery = vehicleModel.AsQueryable();

            var count = vehicleModelQuery.Count();

            pageSize = pageParameters.pageSize;
            pageIndex = pageParameters.pageIndex;

            totalPages = (int)Math.Ceiling(count / (double)pageSize);
           
            paginetedModelQuery = vehicleModelQuery.OrderByDescending(d => d.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize);

            return paginetedModelQuery;
        }
    }
}
