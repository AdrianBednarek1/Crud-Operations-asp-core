using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces.IVehicleDB
{
    public interface IVehicleDB
    {
        DbSet<VehicleMade> vehicleMades { get; }
        DbSet<VehicleModel> vehicleModels { get; }
    }
}
