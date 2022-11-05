using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel
{
    public interface ISortingModel
    {
        SortingHelp sortingHelpModel { get; set; }
        bool descending { get; }
        string nameOfProperty { get; }
        Task SortModel(string sortOrderModel);
    }
}
