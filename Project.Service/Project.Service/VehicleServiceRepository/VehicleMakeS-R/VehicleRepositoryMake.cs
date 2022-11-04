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
        public async Task<IQueryable<VehicleMake>> GetDBQueryMake()
        {
            return vehicleDB.vehicleMakes.AsQueryable();
        }
        public async Task<List<VehicleMake>> GetFullListMake()
        {
            return await vehicleDB.vehicleMakes.ToListAsync();
        }
        public async Task<List<VehicleMake>> ReturnMakeList()
        {
            IQueryable<VehicleMake> paginetedQuery;
            paginetedQuery = pagingMake.paginetedMakeQuery ?? vehicleDB.vehicleMakes.AsQueryable();

            IQueryable<VehicleMake> filteredQuery;
            filteredQuery = filteringMake.filteredMakeQuery ?? vehicleDB.vehicleMakes.AsQueryable();

            IQueryable<VehicleMake> filteredPaginetedQuery;
            filteredPaginetedQuery = paginetedQuery.Intersect(filteredQuery);

            List<VehicleMake> paginetedFilteredSortedList;
            paginetedFilteredSortedList= await filteredPaginetedQuery.ToListAsync();

            if (sortingMake.descending)
            {
                return paginetedFilteredSortedList.OrderByDescending(p => p.GetType().GetProperty(sortingMake.nameOfProperty).GetValue(p)).ToList();
            }
            return paginetedFilteredSortedList.OrderBy(p => p.GetType().GetProperty(sortingMake.nameOfProperty).GetValue(p)).ToList();
        }
        public async Task PagingVehicleMake(int pageIndex, int pageSize)
        {
            await pagingMake.CreateAsync(pageIndex, pageSize);
        }
        public async Task FilterVehicleMake(string searchStringMake, string currentSearchMake, int? pageIndexMake)
        {
            await filteringMake.FilterMake(searchStringMake, currentSearchMake, pageIndexMake);
        }
        public async Task SortVehicleMake(string sortOrderMake)
        {
            await sortingMake.SortMake(sortOrderMake);
        }
        public async Task<VehicleModel> SearchVehicleModel(int id)
        {
            var vehicleModel = await vehicleDB.vehicleModels.SingleOrDefaultAsync(d => d.Id == id);
            return vehicleModel;
        }
        public async Task<List<VehicleMake>> GetVehicleMakes()
        {
            return await vehicleDB.vehicleMakes.ToListAsync();
        }    
        public async Task CreateVehicleMake(VehicleMake? made)
        {
            if (made!=null)
            {
                vehicleDB.vehicleMakes.Add(made);
            }
            await vehicleDB.SaveChangesAsync();
        }
        public async Task DeleteVehicleMake(VehicleMake? made)
        {
            if (made !=null)
            {
                foreach (var item in vehicleDB.vehicleModels)
                {
                    if(item.MakeId == made.Id && item!=null)
                    {
                        vehicleDB.vehicleModels.Remove(item);
                    }
                }
                vehicleDB.vehicleMakes.Remove(made);
            }           
            await vehicleDB.SaveChangesAsync();
        }
        public async Task UpdateVehicleMake(VehicleMake? made)
        {
            if (made != null)
            {
                vehicleDB.vehicleMakes.Single(d => d.Id == made.Id).Id = made.Id;
                vehicleDB.vehicleMakes.Single(d => d.Id == made.Id).Abrv = made.Abrv;
                vehicleDB.vehicleMakes.Single(d => d.Id == made.Id).Name = made.Name;
            }                   
            await vehicleDB.SaveChangesAsync();
        }
        public async Task<VehicleMake> SearchVehicleMake(int id)
        {
            var vehicleMade = await vehicleDB.vehicleMakes.SingleOrDefaultAsync(d => d.Id == id);
            return vehicleMade;
        }
        public async Task<bool> VehicleMakesIsNull()
        {
            if (!await vehicleDB.vehicleMakes.AnyAsync() || vehicleDB.vehicleMakes==null)
            {
                return true;
            }
            return false;
        }
    }
}
