using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleRepository
{
    public interface IVehicleRepositoryModel
    {
        Task<DbSet<VehicleModel>> GetDBQueryModel();
        Task<List<VehicleModel>> GetVehicleModels();
        Task CreateVehicleModel(VehicleModel? model);
        Task DeleteVehicleModel(VehicleModel? model);
        Task UpdateVehicleModel(VehicleModel? model);
        Task<VehicleModel> SearchVehicleModel(int id);
        void DeleteVehicleModelWithoutSaving(VehicleModel? model);
        Task<bool> VehicleModelsIsNull();
    }
}
