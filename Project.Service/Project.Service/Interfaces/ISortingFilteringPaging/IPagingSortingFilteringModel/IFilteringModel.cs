using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel
{
    public interface IFilteringModel
    {
        string? currentSearch { get; }
        IQueryable<VehicleModel>? filterModelQuery { get; }
        Task<IQueryable<VehicleModel>> GetFilterModel(FilterParameters filterParameters);
    }
}
