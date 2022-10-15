using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces.IVehicleDB
{
    public interface IVehicleDB
    {
        DbSet<VehicleMake> vehicleMades { get; }
        DbSet<VehicleModel> vehicleModels { get; }
    }
}
