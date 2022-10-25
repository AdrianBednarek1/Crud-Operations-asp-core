using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.Interfaces.IVehicleService;
using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.VehicleService
{
    public class VehicleServiceModel : IVehicleServiceModel
    {
        private IVehicleRepositoryModel vehicleRepositoryModel;
        public VehicleServiceModel(IVehicleRepositoryModel _vehicleRepositoryModel)
        {
            vehicleRepositoryModel = _vehicleRepositoryModel;
        }
        public async Task<DbSet<VehicleModel>> GetQueryDBmodel()
        {
            return await vehicleRepositoryModel.GetDBQueryModel();
        }
        public async Task<VehicleModel> SearchVehicleModel(int id)
        {
            return await vehicleRepositoryModel.SearchVehicleModel(id);
        }
        public async Task<List<VehicleModel>> GetVehicleModels()
        {
            return await vehicleRepositoryModel.GetVehicleModels();
        }
        public async Task Create(VehicleModel data)
        {
            VehicleModel? model = data;
            await vehicleRepositoryModel.CreateVehicleModel(model);         
        }
        public async Task Update(VehicleModel data)
        {
            VehicleModel? model = data;
            await vehicleRepositoryModel.UpdateVehicleModel(model);           
        }
        public async Task Delete(VehicleModel data)
        {
            VehicleModel? model = data;
            await vehicleRepositoryModel.DeleteVehicleModel(model);
        }
        public async Task<bool> VehicleModelIsNull()
        {
            return await vehicleRepositoryModel.VehicleModelsIsNull();
        }
    }
}
