using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class FilteringMake
    {
        public string? CurrentSearchMake { get; private set; }
        public IQueryable<VehicleMake> SearchFilterMake
            (string SearchString, string CurrentSearch, IQueryable<VehicleMake> vehicleMadesSorting, int? pageIndexMade)
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
                vehicleMadesSorting = vehicleMadesSorting.Where(s => s.Name.Contains(SearchString) || s.Abrv.Contains(SearchString));
            }

            return vehicleMadesSorting;
            
        }     
    }
}
