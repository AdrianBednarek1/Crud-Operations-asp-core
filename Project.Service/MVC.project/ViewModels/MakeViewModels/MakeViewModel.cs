using ZaPrav.NetCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MVC.project.ViewModels.MakeViewModels
{
    public class MakeViewModel : IVehicleMake
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
    }
}
