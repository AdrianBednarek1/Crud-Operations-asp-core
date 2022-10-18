using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleRepository
{
    public interface IVehicleService
    {
        VehicleDB GetQueryDB();
        Task<List<VehicleMake>> GetVehicleMakes();
        Task<VehicleMake> SearchVehicleMake(int id);
        Task<VehicleModel> SearchVehicleModel(int id);
        Task<List<VehicleModel>> GetVehicleModels();
        Task Create(object data);
        Task Update(object data);
        Task Delete(object data);
        Task<bool> VehicleMakeIsNull();
        Task<bool> VehicleModelIsNull();
    }
}



