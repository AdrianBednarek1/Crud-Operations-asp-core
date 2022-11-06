using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel;
using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore;

namespace Project.Service.PagingSortingFiltering.PSFmodel
{
    public class SortingModel : ISortingModel
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
        public async Task<string> GetPropertyNameSort(SortParameters sortParameters)
        {
            string sortOrder = sortParameters.sortOrder;
            SetSortAttributes(sortOrder);
            isDescending = sortOrder?.Contains("Desc") ?? false;
            nameOfProperty = isDescending ? sortOrder.Remove(sortOrder.Length - 4, 4) : sortOrder ?? "Name";

            return nameOfProperty;
        }
        private void SetSortAttributes(string sortOrderModel)
        {
            sortAttributes.CurrentSort = sortOrderModel;
            sortAttributes.NameSort = String.IsNullOrEmpty(sortOrderModel) ? "NameDesc" : "";
            sortAttributes.AbrvSort = sortOrderModel == "Abrv" ? "AbrvDesc" : "Abrv";
            sortAttributes.IdSort = sortOrderModel == "Id" ? "IdDesc" : "Id";
            sortAttributes.ForeignIdSort = sortOrderModel == "MakeId" ? "MakeIdDesc" : "MakeId";
        }
    }
}
