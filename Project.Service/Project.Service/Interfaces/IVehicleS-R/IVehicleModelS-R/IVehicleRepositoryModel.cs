using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel;
using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.PagingSortingFiltering.PSFmodel;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleRepository
{
    public interface IVehicleRepositoryModel
    {
        IFilteringModel filteringModel { get; }
        ISortingModel sortingModel { get; }
        IPagingModel pagingModel { get; }
        Task<List<VehicleModel>> GetVehicleModel();
        Task<List<VehicleModel>> GetVehicleModel(SortParameters sortParameters, FilterParameters filterParameters, PageParameters pageParameters);
        Task Create(VehicleModel? createModel);
        Task Delete(VehicleModel? deleteModel);
        Task Update(VehicleModel? updateModel);
        Task<VehicleModel> GetModelById(int id);
    }
}
