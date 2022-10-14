using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ZaPrav.NetCore.Pages
{
    public class MadeCreator : PageModel
    {
        [BindProperty]
        public VehicleDB.VehicleMade vehicleMade { get; set; }      
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            { 
                return Page();
            }
            await VehicleDB.VehicleService.Create(vehicleMade);
            return RedirectToPage("/Index");
        }
    }
}
