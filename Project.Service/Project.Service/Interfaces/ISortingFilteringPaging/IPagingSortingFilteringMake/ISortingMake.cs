using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public interface ISortingMake
    {
        SortAttributes sortingAttributes { get; }
        bool isDescending { get; }
        string nameOfProperty { get; }
        Task<string> GetPropertyNameSort(SortParameters _sortParameters);
    }
}