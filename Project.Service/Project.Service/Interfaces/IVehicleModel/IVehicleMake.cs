using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces
{
    public interface IVehicleMake
    {
        int Id { get; }
        string Name { get; }
        string Abrv { get; }
        List<VehicleModel>? Models { get; }
    }
}
