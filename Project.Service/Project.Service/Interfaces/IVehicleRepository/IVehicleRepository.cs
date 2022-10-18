using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleService
{
    public interface IVehicleRepository
    {
        VehicleDB GetDBQuery();
        Task<List<VehicleMake>> GetVehicleMades();
        Task<List<VehicleModel>> GetVehicleModels();
        Task CreateVehicleModel(VehicleModel model);
        Task CreateVehicleMade(VehicleMake made);
        Task DeleteVehicleModel(VehicleModel model);
        Task DeleteVehicleMake(VehicleMake made);
        Task UpdateVehicleMake(VehicleMake made);
        Task UpdateVehicleModel(VehicleModel model);
        Task<VehicleMake> SearchVehicleMake(int id);
        Task<VehicleModel> SearchVehicleModel(int id);
        Task<bool> VehicleMakesIsNull();
        Task<bool> VehicleModelsIsNull();
    }
}
