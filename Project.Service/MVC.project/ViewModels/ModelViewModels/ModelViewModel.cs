using ZaPrav.NetCore.Interfaces;

namespace MVC.project.ViewModels.ModelViewModels
{
    public class ModelViewModel : IVehicleModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; } 
        public int MakeId { get; set; }
    }
}
