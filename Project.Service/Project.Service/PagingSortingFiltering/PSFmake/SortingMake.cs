using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class SortingMake //: ISortingMake
    {
        public SortingHelp sortingHelpMake { get; set; }
        public bool descending = false;
        public string nameOfProperty = "Id";
        public SortingMake()
        {
            sortingHelpMake = new SortingHelp();
        }
        public async Task SortMake(string sortOrderMake)
        {
            sortingHelpMake.CurrentSort = sortOrderMake;
            sortingHelpMake.NameSort = String.IsNullOrEmpty(sortOrderMake) ? "NameDesc" : "";
            sortingHelpMake.AbrvSort = sortOrderMake == "Abrv" ? "AbrvDesc" : "Abrv";
            sortingHelpMake.IdSort = sortOrderMake == "Id" ? "IdDesc" : "Id";

            switch (sortOrderMake)
            {
                case "IdDesc":
                    descending = true;
                    nameOfProperty = nameof(VehicleMake.Id);
                    break;
                case "Id":
                    descending = false;
                    nameOfProperty = nameof(VehicleMake.Id);
                    break;
                case "Abrv":
                    descending = false;
                    nameOfProperty = nameof(VehicleMake.Abrv);
                    break;
                case "NameDesc":
                    descending = true;
                    nameOfProperty = nameof(VehicleMake.Name);
                    break;
                case "AbrvDesc":
                    descending = true;
                    nameOfProperty = nameof(VehicleMake.Abrv);
                    break;
                default:
                    nameOfProperty = nameof(VehicleMake.Name);
                    descending = false;
                    break;
            }
        }
    }
}
