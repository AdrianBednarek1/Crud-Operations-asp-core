using Project.Service.PagingSortingFiltering;
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
        public async Task<DbSet<VehicleMake>> GetVehicleMakes()
        {
            return vehicleDB.vehicleMakes;
        }
        public async Task<List<VehicleMake>> SortedFilteredPaginetedListMake()
        {
            IQueryable<VehicleMake> paginetedQuery;
            paginetedQuery = pagingMake.paginetedMakeQuery ?? vehicleDB.vehicleMakes.AsQueryable();

            IQueryable<VehicleMake> filteredQuery;
            filteredQuery = filteringMake.filterQueryMake ?? vehicleDB.vehicleMakes.AsQueryable();

            IQueryable<VehicleMake> filteredPaginetedQuery;
            filteredPaginetedQuery = paginetedQuery.Intersect(filteredQuery);

            List<VehicleMake> paginetedFilteredSortedList;
            paginetedFilteredSortedList = await filteredPaginetedQuery.ToListAsync();

            if (sortingMake.descending)
            {
                return paginetedFilteredSortedList.OrderByDescending(p => p.GetType().GetProperty(sortingMake.nameOfProperty).GetValue(p)).ToList();
            }
            return paginetedFilteredSortedList.OrderBy(p => p.GetType().GetProperty(sortingMake.nameOfProperty).GetValue(p)).ToList();
        }
        public async Task PagingVehicleMake(int pageIndex, int pageSize)
        {
            await pagingMake.CreatePagingMake(pageIndex, pageSize);
        }
        public async Task FilterVehicleMake(string searchStringMake, string currentSearchMake)
        {
            await filteringMake.FilterMake(searchStringMake, currentSearchMake);
        }
        public async Task SortVehicleMake(string sortOrderMake)
        {
            await sortingMake.SortMake(sortOrderMake);
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
        public async Task<bool> VehicleMakeIsNull()
        {
            if (!await vehicleDB.vehicleMakes.AnyAsync() || vehicleDB.vehicleMakes == null)
            {
                return true;
            }
            return false;
        }
    }
}
