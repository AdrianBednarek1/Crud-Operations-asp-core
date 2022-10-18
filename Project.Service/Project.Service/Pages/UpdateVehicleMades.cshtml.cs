using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZaPrav.NetCore.Interfaces.IUpdateVehicleMades;

namespace ZaPrav.NetCore.Pages
{
    public class UpdateVehicleMadesModel : PageModel, IUpdateVehicleMades
    {
        [BindProperty]
        public VehicleDB.VehicleMake made { get; set; }     
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }          
            return RedirectToPage("./Index", "UpdateVehicleMake", made);
        }
    }
}
