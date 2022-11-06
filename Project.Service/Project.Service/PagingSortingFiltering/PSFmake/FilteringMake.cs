using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class FilteringMake //: IFilteringMake
    {
        public string? currentSearchMake { get; private set; }
        public IQueryable<VehicleMake>? filterQueryMake { get; set; }
        public async Task<IQueryable<VehicleMake>> GetFilterMake(FilterParameters filterParameters)
        {
            currentSearchMake = filterParameters.GetCurrentSearch();

            List<VehicleMake> vehicleMake = await VehicleServiceMake.GetVehicleMake();
            filterQueryMake = vehicleMake.AsQueryable();

            if (!String.IsNullOrEmpty(currentSearchMake))
            {
                filterQueryMake = filterQueryMake.Where(s => s.Name.Contains(currentSearchMake) || s.Abrv.Contains(currentSearchMake));
            }
            return filterQueryMake;
        }
    }
}
