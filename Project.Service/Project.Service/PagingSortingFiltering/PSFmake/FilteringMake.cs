using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmake;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class FilteringMake : IFilteringMake
    {
        public string? CurrentSearchMake { get; private set; }
        public IQueryable<VehicleMake> SearchFilterMake
            (string SearchString, string CurrentSearch, IQueryable<VehicleMake> vehicleMakes, int? pageIndexMade)
        {
            if (SearchString != null)
            {
                pageIndexMade = 1;
            }
            else
            {
                SearchString = CurrentSearch;
            }
            CurrentSearchMake = SearchString;

            if (!String.IsNullOrEmpty(SearchString))
            {
                vehicleMakes = vehicleMakes.Where(s => s.Name.Contains(SearchString) || s.Abrv.Contains(SearchString));
            }

            return vehicleMakes;
            
        }     
    }
}
