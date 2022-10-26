using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Ninject;
using Project.Service;
using ZaPrav.NetCore.Interfaces;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Pages
{
    public class ModelCreatorModel : PageModel, IModelCreator
    {
        [BindProperty]
        public int Id { get; set; }
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public string abrv { get; set; }
        [BindProperty]
        public List<VehicleMake> db { get; set; }
        public IMapper mapper;
        public ModelCreatorModel()
        {
            db = new List<VehicleMake>();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ModelState.Clear();
                return Page();
            }

            VehicleModel model = CreateVehicleModel();
            return RedirectToPage("./Index", "CreateVehicleModel", model);
        }

        private VehicleModel CreateVehicleModel()
        {
            VehicleModel vehicleModel = new VehicleModel()
            {
                Abrv = abrv,
                MakeId = Id,
                Name = name
            };

            return vehicleModel;
        }
        public async Task OnGetId(VehicleMake vehicleMake)
        {
            Id = vehicleMake.Id;
        }
        
    }
}
