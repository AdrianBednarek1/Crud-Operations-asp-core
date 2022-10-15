using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleService
{
    public interface IVehicleRepository
    {
        public VehicleDB GetDBQuery();
        public Task<List<VehicleMake>> GetVehicleMades();
        public Task<List<VehicleModel>> GetVehicleModels();

        public Task CreateVehicleModel(VehicleModel model);
        public Task CreateVehicleMade(VehicleMake made);
        public Task DeleteVehicleModel(VehicleModel model);
        public Task DeleteVehicleMade(VehicleMake made);
        public Task UpdateVehicleMade(VehicleMake made);
        public Task UpdateVehicleModel(VehicleModel model);
        public Task<VehicleMake> SearchVehicleMade(int id);
    }
}
