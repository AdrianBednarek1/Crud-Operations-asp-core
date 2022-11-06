using Project.Service.PagingSortingFiltering.Parameters;
using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class SortingMake : ISortingMake
    {
        public SortAttributes sortingAttributes { get; set; }
        public bool isDescending { get; set; }
        public string nameOfProperty { get; set; }
        public SortingMake()
        {
            sortingAttributes = new SortAttributes();
            isDescending = false;
            nameOfProperty = "Id";
        }
        public async Task<string> GetPropertyNameSort(SortParameters _sortParameters)
        {
            string sortOrder = _sortParameters.sortOrder;
            SetSortAttributes(sortOrder);
            isDescending = sortOrder?.Contains("Desc") ?? false;
            nameOfProperty = isDescending ? sortOrder.Remove(sortOrder.Length - 4, 4) : sortOrder ?? "Name";

            return nameOfProperty;
        }
        private void SetSortAttributes(string sortOrder)
        {
            sortingAttributes.CurrentSort = sortOrder;
            sortingAttributes.NameSort = String.IsNullOrEmpty(sortOrder) ? "NameDesc" : "";
            sortingAttributes.AbrvSort = sortOrder == "Abrv" ? "AbrvDesc" : "Abrv";
            sortingAttributes.IdSort = sortOrder == "Id" ? "IdDesc" : "Id";
        }
    }
}
