using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.Interfaces.IVehicleService;
using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.PagingSortingFiltering.PSFmodel;
using System.Data.Entity;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.VehicleService
{
    public class VehicleServiceModel
    {
        private static VehicleRepositoryModel vehicleRepositoryModel = new VehicleRepositoryModel();
        public static async Task<List<VehicleModel>> GetVehicleModel()
        {
            return await vehicleRepositoryModel.GetVehicleModel();
        }
        public static async Task<SortAttributes> ReturnSortingHelp()
        {
            return vehicleRepositoryModel.sortingModel.sortAttributes;
        }
        public static async Task<PagingModel> GetPreviousNextPageModel()
        {
            return vehicleRepositoryModel.pagingModel;
        }
        public static async Task<List<VehicleModel>> GetVehicleModel
            (SortParameters sortParameters, FilterParameters filterParameters, PageParameters pageParameters)
        {
            return await vehicleRepositoryModel.GetVehicleModel(sortParameters, filterParameters, pageParameters);
        }
        public static async Task<string> ReturnCurrentSearch()
        {
            return vehicleRepositoryModel.filteringModel.currentSearch;
        }
        public static async Task<VehicleModel> GetModelById(int id)
        {
            return await vehicleRepositoryModel.GetModelById(id);
        }
        public static async Task Create(VehicleModel data)
        {
            await vehicleRepositoryModel.Create(data);
        }
        public static async Task Update(VehicleModel data)
        {
            await vehicleRepositoryModel.Update(data);
        }
        public static async Task Delete(VehicleModel data)
        {
            await vehicleRepositoryModel.Delete(data);
        }
    }
}
