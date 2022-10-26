using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class FilteringModel : IFilteringModel
    {
        public string? CurrentSearchModel { get; private set; }

        public IQueryable<VehicleModel> SearchFilterModel
            (string SearchString, string CurrentSearch, IQueryable<VehicleModel> vehicleModelSorting, int? pageIndexMade)
        {
            if (SearchString != null)
            {
                pageIndexMade = 1;
            }
            else
            {
                SearchString = CurrentSearch;
            }
            CurrentSearchModel = SearchString;

            if (!String.IsNullOrEmpty(SearchString))
            {
                vehicleModelSorting = vehicleModelSorting.Where
                    (s => s.Name.Contains(SearchString) || s.Abrv.Contains(SearchString) || s.MakeId.ToString().Contains(SearchString));
            }

            return vehicleModelSorting;

        }
    }
}
