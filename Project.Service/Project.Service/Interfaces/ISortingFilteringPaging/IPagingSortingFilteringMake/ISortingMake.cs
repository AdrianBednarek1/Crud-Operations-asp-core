using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public interface ISortingMake
    {
        SortingHelp sortingHelpMake { get; }
        IQueryable<VehicleMake> SortMake(string sortOrderMake, IQueryable<VehicleMake> VehicleMake);
    }
}