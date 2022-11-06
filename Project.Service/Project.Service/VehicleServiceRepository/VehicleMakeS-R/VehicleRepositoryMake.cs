using Project.Service.PagingSortingFiltering;
using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.PagingSortingFiltering.PSFmake;
using Project.Service.VehicleService;
using System.Data.Entity;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleRepositoryMake //: IVehicleRepositoryMake
    {
        private VehicleDB vehicleDB;
        public FilteringMake filteringMake { get; set; }
        public PagingMake pagingMake { get; set; }
        public SortingMake sortingMake { get; set; }
        public VehicleRepositoryMake()
        {
            vehicleDB = VehicleStaticDatabase.vehicleDB;
            filteringMake = new FilteringMake();
            pagingMake = new PagingMake();
            sortingMake = new SortingMake();
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
            List<VehicleMake> filterPageSort = SetOrderBy(intersectFilterPage, orderByProperty);

            return filterPageSort;
        }
        private List<VehicleMake> SetOrderBy(List<VehicleMake> filterPage, string propertyNameSort)
        {
            if (sortingMake.isDescending)
            {
                return filterPage.OrderByDescending(p => p.GetType().GetProperty(propertyNameSort).GetValue(p)).ToList();
            }
            return filterPage.OrderBy(p => p.GetType().GetProperty(propertyNameSort).GetValue(p)).ToList();
        }
        public async Task Create(VehicleMake? make)
        {
            if (make != null)
            {
                vehicleDB.vehicleMakes.Add(make);
            }
            await vehicleDB.SaveChangesAsync();
        }
        public async Task Delete(VehicleMake? make)
        {
            if (make != null)
            {
                foreach (var item in vehicleDB.vehicleModels)
                {
                    if (item.MakeId == make.Id && item != null)
                    {
                        vehicleDB.vehicleModels.Remove(item);
                    }
                }
                vehicleDB.vehicleMakes.Remove(make);
            }
            await vehicleDB.SaveChangesAsync();
        }
        public async Task Update(VehicleMake? make)
        {
            if (make != null)
            {
                vehicleDB.vehicleMakes.Single(d => d.Id == make.Id).Id = make.Id;
                vehicleDB.vehicleMakes.Single(d => d.Id == make.Id).Abrv = make.Abrv;
                vehicleDB.vehicleMakes.Single(d => d.Id == make.Id).Name = make.Name;
            }
            await vehicleDB.SaveChangesAsync();
        }
        public async Task<VehicleMake> GetMakeById(int id)
        {
            var vehicleMake = await vehicleDB.vehicleMakes.SingleOrDefaultAsync(d => d.Id == id);
            return vehicleMake;
        }
    }
}
