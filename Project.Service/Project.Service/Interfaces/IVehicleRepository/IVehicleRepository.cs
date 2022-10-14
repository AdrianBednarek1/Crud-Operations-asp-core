using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleService
{
    public interface IVehicleRepository
    {
        public VehicleDB GetDBQuery();
        public Task<List<VehicleMade>> GetVehicleMades();
        public Task<List<VehicleModel>> GetVehicleModels();

        public Task CreateVehicleModel(VehicleModel model);
        public Task CreateVehicleMade(VehicleMade made);
        public Task DeleteVehicleModel(VehicleModel model);
        public Task DeleteVehicleMade(VehicleMade made);
        public Task UpdateVehicleMade(VehicleMade made);
        public Task UpdateVehicleModel(VehicleModel model);
        public Task<VehicleMade> SearchVehicleMade(int id);
    }
}
