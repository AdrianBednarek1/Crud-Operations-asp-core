using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel;
using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.VehicleService;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class FilteringModel //: IFilteringModel
    {
        public string? currentSearch { get; private set; }
        public IQueryable<VehicleModel>? filterModelQuery { get; set; }
        public async Task<IQueryable<VehicleModel>> GetFilterModel(FilterParameters filterParameters)
        {                
            currentSearch = filterParameters.GetCurrentSearch();

            List<VehicleModel> vehicleModel = await VehicleServiceModel.GetVehicleModel();
            filterModelQuery = vehicleModel.AsQueryable();

            if (!String.IsNullOrEmpty(currentSearch))
            {
                filterModelQuery = filterModelQuery.Where
                    (s => s.Name.Contains(currentSearch) || s.Abrv.Contains(currentSearch) || s.MakeId.ToString().Contains(currentSearch));
            }
            return filterModelQuery;
        }
    }
}
