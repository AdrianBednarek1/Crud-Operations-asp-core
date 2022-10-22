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
        public IQueryable<VehicleModel> SortModel(string sortOrderModel, IQueryable<VehicleModel> VehicleModel)
        {
            sortingHelpModel.CurrentSort = sortOrderModel;
            sortingHelpModel.NameSort = String.IsNullOrEmpty(sortOrderModel) ? "NameDesc" : "";
            sortingHelpModel.AbrvSort = sortOrderModel == "Abrv" ? "AbrvDesc" : "Abrv";
            sortingHelpModel.IdSort = sortOrderModel == "Id" ? "IdDesc" : "Id";
            sortingHelpModel.ForeignIdSort = sortOrderModel == "MadeId" ? "MadeIdDesc" : "MadeId";

            switch (sortOrderModel)
            {
                case "IdDesc":
                    VehicleModel = VehicleModel.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    VehicleModel = VehicleModel.OrderBy(s => s.Id);
                    break;
                case "Abrv":
                    VehicleModel = VehicleModel.OrderBy(s => s.Abrv);
                    break;
                case "NameDesc":
                    VehicleModel = VehicleModel.OrderByDescending(s => s.Name);
                    break;
                case "AbrvDesc":
                    VehicleModel = VehicleModel.OrderByDescending(s => s.Abrv);
                    break;
                case "MadeId":
                    VehicleModel = VehicleModel.OrderBy(s => s.MakeId);
                    break;
                case "MadeIdDesc":
                    VehicleModel = VehicleModel.OrderByDescending(s => s.MakeId);
                    break;
                default:
                    VehicleModel = VehicleModel.OrderBy(s => s.Name);
                    break;
            }
            return VehicleModel;
        }
    }
}
