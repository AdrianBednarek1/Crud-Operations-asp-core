using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleRepository
{
    public interface IVehicleServiceModel
    {
        Task<DbSet<VehicleModel>> GetQueryDBmodel();
        Task<VehicleModel> SearchVehicleModel(int id);
        Task<List<VehicleModel>> GetVehicleModels();
        Task Create(VehicleModel data);
        Task Update(VehicleModel data);
        Task Delete(VehicleModel data);
        Task<bool> VehicleModelIsNull();
    }
}
