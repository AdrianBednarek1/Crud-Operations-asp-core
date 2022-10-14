using Microsoft.AspNetCore.Mvc;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IUpdateVehicleMades
{
    public interface IUpdateVehicleMades
    {
        VehicleMade made { get; }
        Task<IActionResult> OnPostAsync();
    }
}
