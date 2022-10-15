using Microsoft.AspNetCore.Mvc;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces.IUpdateVehicleMades
{
    public interface IUpdateVehicleMades
    {
        VehicleMake made { get; }
        Task<IActionResult> OnPostAsync();
    }
}
