using System.ComponentModel.DataAnnotations;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces
{
    public interface IVehicleModel
    {
         int Id { get; }
         string Name { get;}
         string Abrv { get; }

         [Required]
         VehicleMake Make { get; }
    }
}
