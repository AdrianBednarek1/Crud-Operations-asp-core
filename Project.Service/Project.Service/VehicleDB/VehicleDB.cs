using Project.Service.Interfaces.IVehicleDB;
using System.Data.Entity;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleDB : DbContext, IVehicleDB
    {
        public DbSet<VehicleMade> vehicleMades { get; set; }
        public DbSet<VehicleModel> vehicleModels { get; set; }

    }
}
