using Project.Service.Interfaces;
using ZaPrav.NetCore.Interfaces;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleMake : IVehicleMake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public List<VehicleModel>? IdModel  { get; set; }

    }
}
