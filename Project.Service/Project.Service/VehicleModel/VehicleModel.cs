using System.ComponentModel.DataAnnotations;
using ZaPrav.NetCore.Interfaces;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleModel : IVehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        
        [Required]
        public VehicleMade IdMade { get; set; }
    }
}
