using Microsoft.AspNetCore.Mvc;
using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces.IPages.IIndex
{
    public interface IIndexModel
    {
        Paging<VehicleMake> PaginatedVehicleMakes { get; }
        Paging<VehicleModel> PaginatedVehicleModels { get; }
        SortingHelp SortingMadeHelper { get; }
        SortingHelp SortingModelHelper { get; }
        Task OnGetAsync
            (
            string sortOrderMades,
            string SearchStringMade, string currentFilterMade, int? pageIndexMade,

            string sortOrderModel,
            string SearchStringModel, string currentFilterModel, int? pageIndexModel
            );
        Task<IActionResult> OnPostDeleteAsync(int Id, bool TrueIfModel);
    }
}
