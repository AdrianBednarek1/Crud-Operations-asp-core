using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ZaPrav.NetCore.Interfaces.IPages.IMadeCreator;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Pages
{
    public class MadeCreator : PageModel, IMadeCreator
    {
        [BindProperty]
        public VehicleMake vehicleMade { get; set; }
        public MadeCreator()
        {
            vehicleMade = new VehicleMake();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            { 
                return Page();
            }
            await VehicleService.Create(vehicleMade);
            return RedirectToPage("/Index");
        }
    }
}
