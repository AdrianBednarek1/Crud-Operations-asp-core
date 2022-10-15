using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ZaPrav.NetCore.Interfaces;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleModel : IVehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public int MakeId { get; set; }
        public VehicleMake Make { get; set; }
    }
}
