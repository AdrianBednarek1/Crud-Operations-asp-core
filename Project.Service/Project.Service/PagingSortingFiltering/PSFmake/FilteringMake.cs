using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmake;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class FilteringMake //: IFilteringMake
    {
        public string? currentSearchMake { get; private set; }
        public IQueryable<VehicleMake> filterQueryMake { get; set; }
        public async Task FilterMake(string searchString, string _currentSearch)
        {
            if (searchString == null)
            {
                searchString = _currentSearch;
            }

            currentSearchMake = searchString;

            var vehicleMake = await VehicleServiceMake.GetVehicleMake();
            filterQueryMake = vehicleMake.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                filterQueryMake = filterQueryMake.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString));
            }              
        }     
    }
}
