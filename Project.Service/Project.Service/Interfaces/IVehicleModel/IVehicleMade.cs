﻿using ZaPrav.NetCore.VehicleDB;

namespace ZaPrav.NetCore.Interfaces
{
    public interface IVehicleMade
    {
        int Id { get; }
        string Name { get; }
        string Abrv { get; }
        List<VehicleModel>? IdModel { get; }
    }
}
