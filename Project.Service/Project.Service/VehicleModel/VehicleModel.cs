using System.ComponentModel.DataAnnotations;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        
        [Required]
        public VehicleMade IdMade { get; set; }
    }
}
