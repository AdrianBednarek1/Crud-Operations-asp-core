using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.Interfaces.IVehicleService;
using Project.Service.PagingSortingFiltering.PSFmodel;
using System.Data.Entity;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.VehicleService
{
    public class VehicleServiceModel
    {
        private static VehicleRepositoryModel vehicleRepositoryModel = new VehicleRepositoryModel();
        public static async Task<IQueryable<VehicleModel>> GetQueryDBmodel()
        {
            return await vehicleRepositoryModel.GetDBQueryModel();
        }
        public static async Task SortVehicleModel(string sortOrderModel)
        {
            await vehicleRepositoryModel.SortVehicleModel(sortOrderModel);
        }
        public static async Task FilterVehicleModel(string searchStringModel, string currentSearchModel, int? pageIndexModel)
        {
            await vehicleRepositoryModel.FilterVehicleModel(searchStringModel, currentSearchModel, pageIndexModel);
        }
        public static async Task<SortingHelp> ReturnSortingHelp()
        {
            return vehicleRepositoryModel.sortingModel.sortingHelpModel;
        }
        public static async Task<PagingModel> GetPreviousNextPageModel()
        {
            return vehicleRepositoryModel.pagingModel;
        }
        public static async Task PagingVehicleModel(int pageIndex, int pageSize)
        {
            await vehicleRepositoryModel.PagingVehicleModel(pageIndex, pageSize);
        }
        public static async Task<string> ReturnCurrentSearch()
        {
            return vehicleRepositoryModel.filteringModel.currentSearchModel;
        }
        public static async Task<List<VehicleModel>> ReturnMakeList()
        {
            return await vehicleRepositoryModel.ReturnModelList();
        }
        public static async Task<VehicleModel> SearchVehicleModel(int id)
        {
            return await vehicleRepositoryModel.SearchVehicleModel(id);
        }
        public static async Task Create(VehicleModel data)
        {
            VehicleModel? model = data;
            await vehicleRepositoryModel.CreateVehicleModel(model);
        }
        public static async Task Update(VehicleModel data)
        {
            VehicleModel? model = data;
            await vehicleRepositoryModel.UpdateVehicleModel(model);
        }
        public static async Task Delete(VehicleModel data)
        {
            VehicleModel? model = data;
            await vehicleRepositoryModel.DeleteVehicleModel(model);
        }
        public static async Task<bool> VehicleModelIsNull()
        {
            return await vehicleRepositoryModel.VehicleModelsIsNull();
        }
        public static async Task<bool> VehicleMakeIsNull()
        {
            return await vehicleRepositoryModel.VehicleMakeIsNull();
        }
    }
}
