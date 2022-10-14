using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Project.Service.Interfaces.IPages.IMadeCreator;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Pages
{
    public class MadeCreator : PageModel, IMadeCreator
    {
        [BindProperty]
        public VehicleMade vehicleMade { get; set; }
        public MadeCreator()
        {
            vehicleMade = new VehicleMade();
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
