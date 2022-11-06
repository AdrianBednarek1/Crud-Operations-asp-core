using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel
{
    public interface ISortingModel
    {
        SortAttributes sortingHelpModel { get; set; }
        bool isDescending { get; }
        string nameOfProperty { get; }
        Task SortModel(string sortOrderModel);
    }
}
