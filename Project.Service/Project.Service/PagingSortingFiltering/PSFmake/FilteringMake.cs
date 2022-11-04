using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmake;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class FilteringMake //: IFilteringMake
    {
        public string? currentSearchMake { get; private set; }
        public IQueryable<VehicleMake> filteredMakeQuery { get; set; }
        public async Task FilterMake
            (string searchString, string _currentSearch, int? pageIndexMake)
        {
            if (searchString != null)
            {
                pageIndexMake = 1;
            }
            else
            {
                searchString = _currentSearch;
            }
            currentSearchMake = searchString;

            IQueryable<VehicleMake> makeQuery = await VehicleServiceMake.GetQueryMake();

            if (!String.IsNullOrEmpty(searchString))
            {
                makeQuery = makeQuery.Where(s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString));
            }

            filteredMakeQuery = makeQuery;
            
        }     
    }
}
