using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service.Interfaces;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Pages
{
    public class ModelCreatorModel : PageModel, IModelCreator
    {
        
        private VehicleMade? vehicleMade;
        [BindProperty]
        public List<SelectListItem> VehicleMadesInList { get; set; }
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public string abrv { get; set; }
        public ModelCreatorModel()
        {
            VehicleMadesInList = new List<SelectListItem>();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await RefreshDropDownlist();

            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                return Page();
            }

            await CreateVehicleModel();
            return RedirectToPage("/Index");
        }

        private async Task CreateVehicleModel()
        {
            vehicleMade = await VehicleService.SearchVehicleMade(Id);
            
            VehicleModel vehicleModel = new VehicleModel()
            {
                Name = name,
                Abrv = abrv,
                IdMade = vehicleMade
            };
            await VehicleService.Create(vehicleModel);
        }
        public async Task OnGetAsync()
        {
            await RefreshDropDownlist();      
        }
        private async Task RefreshDropDownlist()
        {
            var db = await VehicleService.GetVehicleMades();
            var ListOfVehicleMades = await VehicleService.GetVehicleMades();

            VehicleMadesInList = db.ConvertAll(a =>
            {
                return new SelectListItem
                {
                    Text = a.Name,
                    Value = a.Id.ToString()
                };
            });
        }
    }
}
