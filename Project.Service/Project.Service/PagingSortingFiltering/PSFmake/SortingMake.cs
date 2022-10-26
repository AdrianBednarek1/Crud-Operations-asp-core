using ZaPrav.NetCore;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.PagingSortingFiltering
{
    public class SortingMake : ISortingMake
    {
        public SortingHelp sortingHelpMake { get; set; }
        public SortingMake()
        {
            sortingHelpMake = new SortingHelp();
        }
        public IQueryable<VehicleMake> SortMake(string sortOrderMake, IQueryable<VehicleMake> VehicleMake)
        {
            sortingHelpMake.CurrentSort = sortOrderMake;
            sortingHelpMake.NameSort = String.IsNullOrEmpty(sortOrderMake) ? "NameDesc" : "";
            sortingHelpMake.AbrvSort = sortOrderMake == "Abrv" ? "AbrvDesc" : "Abrv";
            sortingHelpMake.IdSort = sortOrderMake == "Id" ? "IdDesc" : "Id";

            switch (sortOrderMake)
            {
                case "IdDesc":
                    VehicleMake = VehicleMake.OrderByDescending(s => s.Id);
                    break;
                case "Id":
                    VehicleMake = VehicleMake.OrderBy(s => s.Id);
                    break;
                case "Abrv":
                    VehicleMake = VehicleMake.OrderBy(s => s.Abrv);
                    break;
                case "NameDesc":
                    VehicleMake = VehicleMake.OrderByDescending(s => s.Name);
                    break;
                case "AbrvDesc":
                    VehicleMake = VehicleMake.OrderByDescending(s => s.Abrv);
                    break;
                default:
                    VehicleMake = VehicleMake.OrderBy(s => s.Name);
                    break;
            }
            return VehicleMake;
        }      
    }
}
