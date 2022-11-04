using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmodel
{
    public class SortingModel //: ISortingModel
    {
        public SortingHelp sortingHelpModel { get; set; }
        public bool descending = false;
        public string nameOfProperty = "Id";
        public SortingModel()
        {
            sortingHelpModel = new SortingHelp();
        }
        public async Task SortModel(string sortOrderModel)
        {
            sortingHelpModel.CurrentSort = sortOrderModel;
            sortingHelpModel.NameSort = String.IsNullOrEmpty(sortOrderModel) ? "NameDesc" : "";
            sortingHelpModel.AbrvSort = sortOrderModel == "Abrv" ? "AbrvDesc" : "Abrv";
            sortingHelpModel.IdSort = sortOrderModel == "Id" ? "IdDesc" : "Id";
            sortingHelpModel.ForeignIdSort = sortOrderModel == "MadeId" ? "MadeIdDesc" : "MadeId";

            switch (sortOrderModel)
            {
                case "IdDesc":
                    descending = true;
                    nameOfProperty = nameof(VehicleModel.Id);
                    break;
                case "Id":
                    descending = false;
                    nameOfProperty = nameof(VehicleModel.Id);
                    break;
                case "Abrv":
                    descending = false;
                    nameOfProperty = nameof(VehicleModel.Abrv);
                    break;
                case "NameDesc":
                    descending = true;
                    nameOfProperty = nameof(VehicleModel.Name);
                    break;
                case "AbrvDesc":
                    descending = true;
                    nameOfProperty = nameof(VehicleModel.Abrv);
                    break;
                case "MadeId":
                    descending = false;
                    nameOfProperty = nameof(VehicleModel.MakeId);
                    break;
                case "MadeIdDesc":
                    descending = true;
                    nameOfProperty = nameof(VehicleModel.MakeId);
                    break;
                default:
                    nameOfProperty = nameof(VehicleModel.Name);
                    descending = false;
                    break;
            }
        }
    }
}
