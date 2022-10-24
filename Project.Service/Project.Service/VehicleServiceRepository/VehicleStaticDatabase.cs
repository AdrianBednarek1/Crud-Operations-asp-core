using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.VehicleService
{
    public class VehicleStaticDatabase
    {       
        public static VehicleDB vehicleDB { get; set; } = new VehicleDB();
    }
}
