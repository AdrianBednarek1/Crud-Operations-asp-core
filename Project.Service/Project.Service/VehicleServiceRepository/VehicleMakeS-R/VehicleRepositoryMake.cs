using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmake;
using Project.Service.Interfaces.IVehicleService;
using Project.Service.PagingSortingFiltering;
using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.PagingSortingFiltering.PSFmake;
using Project.Service.VehicleService;
using System.Data.Entity;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleRepositoryMake : IVehicleRepositoryMake
    {
        private VehicleDB vehicleDB;
        public IFilteringMake filteringMake { get; set; }
        public IPagingMake pagingMake { get; set; }
        public ISortingMake sortingMake { get; set; }
        public VehicleRepositoryMake(ISortingMake _sortingMake, IPagingMake _pagingMake, IFilteringMake _filteringMake)
        {
            vehicleDB = VehicleStaticDatabase.vehicleDB;
            filteringMake = _filteringMake;
            pagingMake = _pagingMake;
            sortingMake = _sortingMake;
        }
        public async Task<List<VehicleMake>> GetVehicleMake()
        {
            return await vehicleDB.vehicleMakes.ToListAsync();
        }
        public async Task<List<VehicleMake>> GetVehicleMake
            (PageParameters pageParameters, FilterParameters filterParameters, SortParameters sortParameters)
        {
            IQueryable<VehicleMake> pageQuery = await pagingMake.GetPageMake(pageParameters);
            IQueryable<VehicleMake> filterQuery = await filteringMake.GetFilterMake(filterParameters);

            string orderByProperty = await sortingMake.GetPropertyNameSort(sortParameters);

            List<VehicleMake> intersectFilterPage = pageQuery.Intersect(filterQuery).ToList();
            List<VehicleMake> filterPageSort = GetSortList(intersectFilterPage, orderByProperty);

            return filterPageSort;
        }
        private List<VehicleMake> GetSortList(List<VehicleMake> filterPage, string propertyNameSort)
        {
            if (sortingMake.isDescending)
            {
                return filterPage.OrderByDescending(p => p.GetType().GetProperty(propertyNameSort).GetValue(p)).ToList();
            }
            return filterPage.OrderBy(p => p.GetType().GetProperty(propertyNameSort).GetValue(p)).ToList();
        }
        public async Task Create(VehicleMake? make)
        {
            if (make == null) return;

            vehicleDB.vehicleMakes.Add(make);
            await vehicleDB.SaveChangesAsync();
        }
        public async Task Delete(VehicleMake? make)
        {
            if (make == null) return;

            var deleteModels = vehicleDB.vehicleModels.Where(_deleteModels => _deleteModels.MakeId == make.Id);
            vehicleDB.vehicleModels.RemoveRange(deleteModels);
            vehicleDB.vehicleMakes.Remove(make);
            await vehicleDB.SaveChangesAsync();
        }
        public async Task Update(VehicleMake? make)
        {
            if (make == null) return;

            var itemForUpdate = vehicleDB.vehicleMakes.Single(d => d.Id == make.Id);
            vehicleDB.Entry(itemForUpdate).CurrentValues.SetValues(make);
            await vehicleDB.SaveChangesAsync();
        }
        public async Task<VehicleMake> GetMakeById(int id)
        {
            var vehicleMake = await vehicleDB.vehicleMakes.SingleOrDefaultAsync(d => d.Id == id);
            return vehicleMake;
        }
    }
}
