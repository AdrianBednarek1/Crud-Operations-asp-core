using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel
{
    public interface ISortingModel
    {
        SortAttributes sortAttributes { get; }
        bool isDescending { get; }
        string nameOfProperty { get; }
        Task<string> GetPropertyNameSort(SortParameters sortParameters);
    }
}
