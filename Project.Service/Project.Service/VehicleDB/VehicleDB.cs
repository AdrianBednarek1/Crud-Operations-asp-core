using System.Data.Entity;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleDB : DbContext
    {
        public DbSet<VehicleMade> vehicleMades { get; set; }
        public DbSet<VehicleModel> vehicleModels { get; set; }

    }
}
