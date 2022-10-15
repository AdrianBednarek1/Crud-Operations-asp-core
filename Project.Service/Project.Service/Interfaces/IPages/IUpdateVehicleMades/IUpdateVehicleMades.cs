using Microsoft.AspNetCore.Mvc;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces.IUpdateVehicleMades
{
    public interface IUpdateVehicleMades
    {
        VehicleMade made { get; }
        Task<IActionResult> OnPostAsync();
    }
}
