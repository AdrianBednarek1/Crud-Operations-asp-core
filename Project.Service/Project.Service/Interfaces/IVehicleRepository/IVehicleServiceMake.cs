using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleRepository
{
    public interface IVehicleServiceMake
    {
        DbSet<VehicleMake> GetQueryDBmake();
        Task<List<VehicleMake>> GetVehicleMakes();
        Task<VehicleMake> SearchVehicleMake(int id);
        Task Create(VehicleMake data);
        Task Update(VehicleMake data);
        Task Delete(VehicleMake data);
        Task<bool> VehicleMakeIsNull();
    }
}



