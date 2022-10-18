using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ninject;
using Project.Service;
using System.Reflection;
using ZaPrav.NetCore.Interfaces;
using ZaPrav.NetCore.Interfaces.IPages.IMadeCreator;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Pages
{
    public class MadeCreator : PageModel, IMadeCreator
    {
        [BindProperty]
        public VehicleMake vehicleMake { get; set; }
       
        public MadeCreator()
        {
            vehicleMake = new VehicleMake();
        }
        public IActionResult OnPostAsync()
        {         
            if (!ModelState.IsValid)
            { 
                return Page();
            }
            return RedirectToPage("./Index","CreateVehicleMake", vehicleMake);
        }
    }
}
