using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging
{
    public interface IPagingSortingFiltering
    {
        SortingHelp SortingMakeHelper { get; }
        SortingHelp SortingModelHelper { get; }
        PaginatedList<VehicleMake>? PaginatedVehicleMades { get; }
        PaginatedList<VehicleModel>? PaginatedVehicleModel { get; }
        Task<PaginatedList<VehicleMake>> VehicleMadeSFP
            (string sortOrderMades, string SearchStringMade, string currentFilterMade, int? pageIndexMade);
        Task<PaginatedList<VehicleModel>> VehicleModelSFP
            (string sortOrderModel, string SearchStringModel, string currentFilterModel, int? pageIndexModel);
    }
}
