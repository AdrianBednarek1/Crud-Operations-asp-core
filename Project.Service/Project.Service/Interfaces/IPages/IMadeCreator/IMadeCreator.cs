using Microsoft.AspNetCore.Mvc;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces.IPages.IMadeCreator
{
    public interface IMadeCreator
    {
        VehicleMake vehicleMade { get; }
        Task<IActionResult> OnPostAsync();
    }
}
