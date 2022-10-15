using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging
{
    public interface IPagingSortingFiltering
    {
        SortingHelp SortingMadeHelper { get; }
        SortingHelp SortingModelHelper { get; }
        PaginatedList<VehicleMade>? PaginatedVehicleMades { get; }
        PaginatedList<VehicleModel>? PaginatedVehicleModel { get; }
        Task<PaginatedList<VehicleMade>> VehicleMadeSFP
            (string sortOrderMades, string SearchStringMade, string currentFilterMade, int? pageIndexMade);
        Task<PaginatedList<VehicleModel>> VehicleModelSFP
            (string sortOrderModel, string SearchStringModel, string currentFilterModel, int? pageIndexModel);
    }
}
