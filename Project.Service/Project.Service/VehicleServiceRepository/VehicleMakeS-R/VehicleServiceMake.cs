using AutoMapper;
using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.PagingSortingFiltering.PSFmake;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleServiceMake
    {
        private static VehicleRepositoryMake vehicleRepositoryMake = new VehicleRepositoryMake();
        public static async Task<List<VehicleMake>> GetVehicleMake()
        {
            return await vehicleRepositoryMake.GetVehicleMake();
        }
        public static async Task<List<VehicleMake>> GetVehicleMake
            (PageParameters pageParameters, FilterParameters filterParameters, SortParameters sortParameters)
        {
            return await vehicleRepositoryMake.GetVehicleMake(pageParameters, filterParameters, sortParameters);
        }
        public static async Task<SortAttributes> ReturnSortingHelp()
        {
            return vehicleRepositoryMake.sortingMake.sortingAttributes;
        }
        public static async Task<string> ReturnCurrentSearch()
        {
            return vehicleRepositoryMake.filteringMake.currentSearchMake;
        }
        public static async Task<PagingMake> GetPreviousNextPageMake()
        {
            return vehicleRepositoryMake.pagingMake;
        }
        public static async Task<VehicleMake> GetByIdMake(int id)
        {
            return await vehicleRepositoryMake.GetMakeById(id);
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
