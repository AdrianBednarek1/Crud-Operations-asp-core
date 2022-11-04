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
        public static async Task<List<VehicleMake>> GetFullListMake()
        {
            return await vehicleRepositoryMake.GetFullListMake();
        }
        public static async Task<IQueryable<VehicleMake>> GetQueryMake()
        {
            return await vehicleRepositoryMake.GetDBQueryMake();
        }
        public static async Task SortVehicleMake(string sortOrderMake)
        {
            await vehicleRepositoryMake.SortVehicleMake(sortOrderMake);
        }
        public static async Task FilterVehicleMake(string searchStringMake, string currentSearchMake, int? pageIndexMake)
        {
            await vehicleRepositoryMake.FilterVehicleMake(searchStringMake, currentSearchMake, pageIndexMake);
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
        public static async Task<VehicleMake> SearchVehicleMake(int id)
        {
            return await vehicleRepositoryMake.SearchVehicleMake(id);
        }
        public static async Task<List<VehicleMake>> ReturnMakeList()
        {
            return await vehicleRepositoryMake.ReturnMakeList();
        }
        public static async Task Create(VehicleMake data)
        {
            VehicleMake? made = data;
            await vehicleRepositoryMake.CreateVehicleMake(made);
        }
        public static async Task Update(VehicleMake data)
        {
            VehicleMake? made = data;
            await vehicleRepositoryMake.UpdateVehicleMake(made);
        }
        public static async Task Delete(int id)
        {
            VehicleMake? make = await SearchVehicleMake(id);
            await vehicleRepositoryMake.DeleteVehicleMake(make);
        }
    }
}
