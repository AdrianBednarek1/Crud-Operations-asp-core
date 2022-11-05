using Project.Service;
using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.Interfaces.IVehicleService;
using Project.Service.PagingSortingFiltering.PSFmake;
using System.Data.Entity;
using System.Linq;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleServiceMake //: IVehicleServiceMake
    {
        private static VehicleRepositoryMake vehicleRepositoryMake = new VehicleRepositoryMake();
        public static async Task<DbSet<VehicleMake>> GetVehicleMake()
        {
            return await vehicleRepositoryMake.GetVehicleMakes();
        }
        public static async Task SortVehicleMake(string sortOrderMake)
        {
            await vehicleRepositoryMake.SortVehicleMake(sortOrderMake);
        }
        public static async Task FilterVehicleMake(string searchStringMake, string currentSearchMake)
        {
            await vehicleRepositoryMake.FilterVehicleMake(searchStringMake, currentSearchMake);
        }
        public static async Task<SortingHelp> ReturnSortingHelp()
        {
            return vehicleRepositoryMake.sortingMake.sortingHelpMake;
        }
        public static async Task<string> ReturnCurrentSearch()
        {
            return vehicleRepositoryMake.filteringMake.currentSearchMake;
        }
        public static async Task<PagingMake> GetPreviousNextPageMake()
        {
            return vehicleRepositoryMake.pagingMake;
        }
        public static async Task PagingVehicleMake(int pageIndex, int pageSize)
        {
            await vehicleRepositoryMake.PagingVehicleMake(pageIndex, pageSize);
        }
        public static async Task<VehicleMake> GetByIdMake(int id)
        {
            return await vehicleRepositoryMake.GetMakeById(id);
        }
        public static async Task<List<VehicleMake>> SortedFilteredPaginetedListMake()
        {
            return await vehicleRepositoryMake.SortedFilteredPaginetedListMake();
        }
        public static async Task Create(VehicleMake make)
        {
            await vehicleRepositoryMake.Create(make);
        }
        public static async Task Update(VehicleMake make)
        {
            await vehicleRepositoryMake.Update(make);
        }
        public static async Task Delete(int id)
        {
            VehicleMake? make = await GetByIdMake(id);
            await vehicleRepositoryMake.Delete(make);
        }
    }
}
