using Project.Service.PagingSortingFiltering;
using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.PagingSortingFiltering.PSFmake;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.IVehicleService
{
    public interface IVehicleRepositoryMake
    {
        FilteringMake filteringMake { get; }
        PagingMake pagingMake { get; }
        SortingMake sortingMake { get; }
        Task<List<VehicleMake>> GetVehicleMake();
        Task<List<VehicleMake>> GetVehicleMake(PageParameters pageParameters, FilterParameters filterParameters, SortParameters sortParameters);
        Task Create(VehicleMake? make);
        Task Delete(VehicleMake? make);
        Task Update(VehicleMake? make);
        Task<VehicleMake> GetMakeById(int id);
    }
}
