using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmodel
{
    public class SortingModel
    {
        public SortingHelp sortingHelpModel { get; set; }

        public SortingModel()
        {
            sortingHelpModel = new SortingHelp();
        }
        public IQueryable<VehicleModel> SortModel(string sortOrderModel, IQueryable<VehicleModel> SortVehicleModel)
        {
            sortingHelpModel.NameSort = String.IsNullOrEmpty(sortOrderModel) ? "NameDesc" : "";
            sortingHelpModel.AbrvSort = sortOrderModel == "Abrv" ? "AbrvDesc" : "Abrv";
            sortingHelpModel.IdSort = sortOrderModel == "Id" ? "IdDesc" : "Id";
            sortingHelpModel.ForeignIdSort = sortOrderModel == "MadeId" ? "MadeIdDesc" : "MadeId";

            switch (sortOrderModel)
            {
                case "IdDesc":
                    SortVehicleModel = SortVehicleModel.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    SortVehicleModel = SortVehicleModel.OrderBy(s => s.Id);
                    break;
                case "Abrv":
                    SortVehicleModel = SortVehicleModel.OrderBy(s => s.Abrv);
                    break;
                case "NameDesc":
                    SortVehicleModel = SortVehicleModel.OrderByDescending(s => s.Name);
                    break;
                case "AbrvDesc":
                    SortVehicleModel = SortVehicleModel.OrderByDescending(s => s.Abrv);
                    break;
                case "MadeId":
                    SortVehicleModel = SortVehicleModel.OrderBy(s => s.MakeId);
                    break;
                case "MadeIdDesc":
                    SortVehicleModel = SortVehicleModel.OrderByDescending(s => s.MakeId);
                    break;
                default:
                    SortVehicleModel = SortVehicleModel.OrderBy(s => s.Name);
                    break;
            }
            return SortVehicleModel;
        }
    }
}
