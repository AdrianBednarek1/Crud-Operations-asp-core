using System.ComponentModel.DataAnnotations;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces
{
    public interface IVehicleModel
    {
         int Id { get; }
         string Name { get;}
         string Abrv { get; }

         [Required]
         VehicleMade IdMade { get; }
    }
}
