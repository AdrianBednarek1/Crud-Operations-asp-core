using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel;
using Project.Service.VehicleService;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class FilteringModel //: IFilteringModel
    {
        public string? currentSearchModel { get; private set; }
        public IQueryable<VehicleModel> filteredModelQuery { get; set; }
        public async Task FilterModel
            (string searchString, string currentSearch, int? pageIndexMade)
        {
            if (searchString != null)
            {
                pageIndexMade = 1;
            }
            else
            {
                searchString = currentSearch;
            }
            currentSearchModel = searchString;

            IQueryable<VehicleModel> modelQuery = await VehicleServiceModel.GetQueryDBmodel();

            if (!String.IsNullOrEmpty(searchString))
            {
                modelQuery = modelQuery.Where
                    (s => s.Name.Contains(searchString) || s.Abrv.Contains(searchString) || s.MakeId.ToString().Contains(searchString));
            }

            filteredModelQuery = modelQuery;
        }
    }
}
