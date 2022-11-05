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
        public static async Task<DbSet<VehicleModel>> GetVehicleModel()
        {
            return await vehicleRepositoryModel.GetVehicleModel();
        }
        public static async Task SortVehicleModel(string sortOrderModel)
        {
            await vehicleRepositoryModel.SortVehicleModel(sortOrderModel);
        }
        public static async Task FilterVehicleModel(string searchStringModel, string currentSearchModel)
        {
            await vehicleRepositoryModel.FilterVehicleModel(searchStringModel, currentSearchModel);
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
        public static async Task<List<VehicleModel>> PaginatedFilteredSortedModelList()
        {
            return await vehicleRepositoryModel.PaginatedFilteredSortedModelList();
        }
        public static async Task<VehicleModel> GetModelById(int id)
        {
            return await vehicleRepositoryModel.GetModelById(id);
        }
        public static async Task Create(VehicleModel data)
        {
            await vehicleRepositoryModel.CreateVehicleModel(data);
        }
        public static async Task Update(VehicleModel data)
        {
            await vehicleRepositoryModel.UpdateVehicleModel(data);
        }
        public static async Task Delete(VehicleModel data)
        {
            await vehicleRepositoryModel.DeleteVehicleModel(data);
        }
        public static async Task<bool> VehicleMakeIsNull()
        {
            return await vehicleRepositoryModel.VehicleMakeIsNull();
        }
    }
}
