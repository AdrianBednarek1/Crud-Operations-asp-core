using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces
{
    public interface IModelCreator
    {       
        int Id { get; }    
        string name { get; }       
        string abrv { get; }
        Task<IActionResult> OnPostAsync();
        Task OnGetId(VehicleMake vehicleMake);
    }
}

