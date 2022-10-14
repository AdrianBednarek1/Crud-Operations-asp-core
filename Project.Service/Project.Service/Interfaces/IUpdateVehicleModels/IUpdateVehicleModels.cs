using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IUpdateVehicleModels
{
    public interface IUpdateVehicleModels
    {
        string name { get; }
        string abrv { get; }
        int id { get; set; }
        int VehicleMadeID { get; set; }
        List<SelectListItem> VehicleMadesInList { get; set; }
        Task<IActionResult> OnPostAsync();
    }
}
