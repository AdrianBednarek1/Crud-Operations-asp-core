using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Project.Service.Interfaces.IVehicleRepository;
using ZaPrav.NetCore.Interfaces.IUpdateVehicleModels;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Pages
{
    public class UpdateVehicleModelsModel : PageModel, IUpdateVehicleModels
    {
        private VehicleMake vehicleMade;
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public string abrv { get; set; }
        [BindProperty]
        public int id { get; set; }
        [BindProperty]
        public int VehicleMakeID { get; set; }
        [BindProperty]
        public List<SelectListItem> VehicleMadesInList { get; set; }
        private IVehicleServiceMake vehicleServiceMake;
        public UpdateVehicleModelsModel
            (
            IVehicleServiceMake _vehicleServiceMake
            )
        {
            VehicleMadesInList = new List<SelectListItem>();
            vehicleMade = new VehicleMake();
            vehicleServiceMake = _vehicleServiceMake;
        }
        public async Task<IActionResult> OnPostAsync()
        {
            await RefreshDropDownlist();
            if (!ModelState.IsValid)
            {
                return Page();
            }
           
            VehicleModel model = await UpdateVehicleModel();
            return RedirectToPage("./Index", "UpdateVehicleModel", model);
        }
        private async Task<VehicleModel> UpdateVehicleModel()
        {
            vehicleMade = await vehicleServiceMake.SearchVehicleMake(VehicleMakeID);

            VehicleModel model = new VehicleModel()
            {
                Id = id,
                Name = name,
                Abrv = abrv,
                MakeId = vehicleMade.Id
            };

            return model;
        }
        private async Task RefreshDropDownlist()
        {
            var db = await vehicleServiceMake.GetVehicleMakes();

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
