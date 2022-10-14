namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleMade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public List<VehicleModel>? IdModel  { get; set; }

    }
}
