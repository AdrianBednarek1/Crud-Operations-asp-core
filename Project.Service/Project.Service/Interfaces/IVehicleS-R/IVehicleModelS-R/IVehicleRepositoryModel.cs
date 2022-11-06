using Project.Service.PagingSortingFiltering;
using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.PagingSortingFiltering.PSFmodel;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleRepository
{
    public interface IVehicleRepositoryModel
    {
        FilteringModel filteringModel { get; }
        SortingModel sortingModel { get; }
        PagingModel pagingModel { get; }
        Task<List<VehicleModel>> GetVehicleModel();
        Task<List<VehicleModel>> GetVehicleModel(SortParameters sortParameters, FilterParameters filterParameters, PageParameters pageParameters);
        Task Create(VehicleModel? createModel);
        Task Delete(VehicleModel? deleteModel);
        Task Update(VehicleModel? updateModel);
        Task<VehicleModel> GetModelById(int id);
    }
}
