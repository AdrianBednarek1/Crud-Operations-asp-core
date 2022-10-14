using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.Entity;

namespace ZaPrav.NetCore.Pages
{
    public class UpdateVehicleMadesModel : PageModel
    {
        [BindProperty]
        public VehicleDB.VehicleMade made { get; set; }     
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await VehicleDB.VehicleService.Update(made);

            return RedirectToPage("./Index");
        }
    }
}
