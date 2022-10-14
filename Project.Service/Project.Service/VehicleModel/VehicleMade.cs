﻿using Project.Service.Interfaces;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleMade : IVehicleMade
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abrv { get; set; }
        public List<VehicleModel>? IdModel  { get; set; }

    }
}
