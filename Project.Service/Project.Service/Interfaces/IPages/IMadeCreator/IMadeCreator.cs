using Microsoft.AspNetCore.Mvc;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces.IPages.IMadeCreator
{
    public interface IMadeCreator
    {
        VehicleMade vehicleMade { get; }
        Task<IActionResult> OnPostAsync();
    }
}
