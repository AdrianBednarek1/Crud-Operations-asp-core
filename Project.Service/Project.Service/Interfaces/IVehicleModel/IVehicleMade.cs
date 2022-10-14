using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.Interfaces
{
    public interface IVehicleMade
    {
        int Id { get; }
        string Name { get; }
        string Abrv { get; }
        List<VehicleModel>? IdModel { get; }
    }
}
