using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZaPrav.NetCore.Interfaces.IUpdateVehicleModels
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
