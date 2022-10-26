using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleService
{
    public interface IVehicleRepositoryMake
    {
        Task<DbSet<VehicleMake>> GetDBQueryMake();
        Task<List<VehicleMake>> GetVehicleMakes();
        Task CreateVehicleMake(VehicleMake make);
        Task DeleteVehicleMake(VehicleMake make);
        Task UpdateVehicleMake(VehicleMake make);
        Task<VehicleMake> SearchVehicleMake(int id);
        Task<bool> VehicleMakesIsNull();
    }
}
