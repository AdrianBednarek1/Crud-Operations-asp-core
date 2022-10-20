using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class SortingMake
    {
        public SortingHelp sortingHelpMake { get; set; }
        public SortingMake()
        {
            sortingHelpMake = new SortingHelp();
        }
        public IQueryable<VehicleMake> SortMake(string sortOrderMake, IQueryable<VehicleMake> SortVehicleMake)
        {
            sortingHelpMake.CurrentSort = sortOrderMake;
            sortingHelpMake.NameSort = String.IsNullOrEmpty(sortOrderMake) ? "NameDesc" : "";
            sortingHelpMake.AbrvSort = sortOrderMake == "Abrv" ? "AbrvDesc" : "Abrv";
            sortingHelpMake.IdSort = sortOrderMake == "Id" ? "IdDesc" : "Id";

            switch (sortOrderMake)
            {
                case "IdDesc":
                    SortVehicleMake = SortVehicleMake.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    SortVehicleMake = SortVehicleMake.OrderBy(s => s.Id);
                    break;
                case "Abrv":
                    SortVehicleMake = SortVehicleMake.OrderBy(s => s.Abrv);
                    break;
                case "NameDesc":
                    SortVehicleMake = SortVehicleMake.OrderByDescending(s => s.Name);
                    break;
                case "AbrvDesc":
                    SortVehicleMake = SortVehicleMake.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    SortVehicleMake = SortVehicleMake.OrderBy(s => s.Name);
                    break;
            }
            return SortVehicleMake;
        }      
    }
}
