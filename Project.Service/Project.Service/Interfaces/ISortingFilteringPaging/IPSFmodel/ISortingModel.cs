using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel
{
    public interface ISortingModel
    {
        public SortingHelp sortingHelpModel { get; set; }
        public IQueryable<VehicleModel> SortModel(string sortOrderModel, IQueryable<VehicleModel> VehicleModel);
    }
}
