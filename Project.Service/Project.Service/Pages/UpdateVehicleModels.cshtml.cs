using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service.Interfaces.IUpdateVehicleModels;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Pages
{
    public class UpdateVehicleModelsModel : PageModel, IUpdateVehicleModels
    {
        private VehicleMade vehicleMade;
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public string abrv { get; set; }
        [BindProperty]
        public int id { get; set; }
        [BindProperty]
        public int VehicleMadeID { get; set; }
        [BindProperty]
        public List<SelectListItem> VehicleMadesInList { get; set; }
        public async Task<IActionResult> OnPostAsync()
        {
            await RefreshDropDownlist();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await UpdateVehicleModel();

            return RedirectToPage("./Index");
        }

        private async Task UpdateVehicleModel()
        {
            vehicleMade = await VehicleService.SearchVehicleMade(VehicleMadeID);

            VehicleModel model = new VehicleModel()
            {
                Id = id,
                Name = name,
                Abrv = abrv,
                IdMade = vehicleMade
            };

            await VehicleService.Update(model);
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
