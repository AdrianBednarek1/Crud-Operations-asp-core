using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel;
using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering.PSFmodel
{
    public class SortingModel //: ISortingModel
    {
        public SortAttributes sortAttributes { get; set; }
        public bool isDescending { get; set; }
        public string nameOfProperty { get; set; }
        public SortingModel()
        {
            isDescending = false;
            nameOfProperty = "Id";
            sortAttributes = new SortAttributes();
        }
        public async Task<string> SortModel(SortParameters sortParameters)
        {
            string sortOrderModel = sortParameters.sortOrder;

            SetSortAttributes(sortOrderModel);

            switch (sortOrderModel)
            {
                case "IdDesc":
                    isDescending = true;
                    nameOfProperty = nameof(VehicleModel.Id);
                    break;
                case "Id":
                    isDescending = false;
                    nameOfProperty = nameof(VehicleModel.Id);
                    break;
                case "Abrv":
                    isDescending = false;
                    nameOfProperty = nameof(VehicleModel.Abrv);
                    break;
                case "NameDesc":
                    isDescending = true;
                    nameOfProperty = nameof(VehicleModel.Name);
                    break;
                case "AbrvDesc":
                    isDescending = true;
                    nameOfProperty = nameof(VehicleModel.Abrv);
                    break;
                case "MadeId":
                    isDescending = false;
                    nameOfProperty = nameof(VehicleModel.MakeId);
                    break;
                case "MadeIdDesc":
                    isDescending = true;
                    nameOfProperty = nameof(VehicleModel.MakeId);
                    break;
                default:
                    nameOfProperty = nameof(VehicleModel.Name);
                    isDescending = false;
                    break;
            }
            return nameOfProperty;
        }
        private void SetSortAttributes(string sortOrderModel)
        {
            sortAttributes.CurrentSort = sortOrderModel;
            sortAttributes.NameSort = String.IsNullOrEmpty(sortOrderModel) ? "NameDesc" : "";
            sortAttributes.AbrvSort = sortOrderModel == "Abrv" ? "AbrvDesc" : "Abrv";
            sortAttributes.IdSort = sortOrderModel == "Id" ? "IdDesc" : "Id";
            sortAttributes.ForeignIdSort = sortOrderModel == "MadeId" ? "MadeIdDesc" : "MadeId";
        }
    }
}
