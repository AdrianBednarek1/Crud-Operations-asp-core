using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleDB
{
    public interface IVehicleDB
    {
        DbSet<VehicleMade> vehicleMades { get; }
        DbSet<VehicleModel> vehicleModels { get; }
    }
}
