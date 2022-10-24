using ZaPrav.NetCore.VehicleDB;

namespace MVC.project.ViewModels.MakeViewModels
{
    public class MakeListViewModel
    {
        public List<VehicleMake> vehicleMakeList { get; set; }
        public MakeListViewModel()
        {
            vehicleMakeList = new List<VehicleMake>();
        }
    }
}
