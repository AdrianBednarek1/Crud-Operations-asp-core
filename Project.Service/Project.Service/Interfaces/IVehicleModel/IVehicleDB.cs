using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces.IVehicleDB
{
    public interface IVehicleDB : IDisposable
    {
        DbSet<VehicleMake> vehicleMakes { get; }
        DbSet<VehicleModel> vehicleModels { get; }
    }
}
