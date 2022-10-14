using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ZaPrav.NetCore.Pages
{
    public class UpdateVehicleModelsModel : PageModel
    {
        private VehicleDB.VehicleMade vehicleMade;
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
        //[BindProperty]
        //public VehicleDB.VehicleModel model { get; set; }
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
            vehicleMade = await VehicleDB.VehicleRepository.SearchVehicleMade(VehicleMadeID);

            VehicleDB.VehicleModel model = new VehicleDB.VehicleModel()
            {
                Id = id,
                Name = name,
                Abrv = abrv,
                IdMade = vehicleMade
            };

            await VehicleDB.VehicleService.Update(model);
        }

        private async Task RefreshDropDownlist()
        {
            var db = await VehicleDB.VehicleRepository.GetVehicleMades();
            var ListOfVehicleMades = await VehicleDB.VehicleRepository.GetVehicleMades();

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
