using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging
{
    public interface IPagingSortingFiltering
    {
        SortingHelp SortingMakeHelper { get; }
        SortingHelp SortingModelHelper { get; }
        Paging<VehicleMake>? PaginatedVehicleMades { get; }
        Paging<VehicleModel>? PaginatedVehicleModel { get; }
        Task<Paging<VehicleMake>> VehicleMadeSFP
            (string sortOrderMades, string SearchStringMade, string currentFilterMade, int? pageIndexMade);
        Task<Paging<VehicleModel>> VehicleModelSFP
            (string sortOrderModel, string SearchStringModel, string currentFilterModel, int? pageIndexModel);
    }
}
