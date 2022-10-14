namespace ZaPrav.NetCore.VehicleDB
{
    public static class VehicleService
    {
        public static async Task Create(object data)
        {
            if(TrueIfModel(data))
            {
                VehicleModel? model = data as VehicleModel;
                await VehicleRepository.CreateVehicleModel(model);
            }
            else
            {
                VehicleMade? made = data as VehicleMade;
                await VehicleRepository.CreateVehicleMade(made);
            }
        }
        public static async Task Update(object data)
        {
            if (TrueIfModel(data))
            {
                VehicleModel? model = data as VehicleModel;
                await VehicleRepository.UpdateVehicleModel(model);
            }
            else
            {
                VehicleMade? made = data as VehicleMade;
                await VehicleRepository.UpdateVehicleMade(made);
            }
        }
        public static async Task Delete(object data)
        {
            if (TrueIfModel(data))
            {
                VehicleModel? model = data as VehicleModel;
                await VehicleRepository.DeleteVehicleModel(model);
            }
            else
            {
                VehicleMade? made = data as VehicleMade;
                await VehicleRepository.DeleteVehicleMade(made);
            }
        }
        private static bool TrueIfModel(object data)
        {
            if(data.GetType() == typeof(VehicleModel))
            {
                return true;
            }
            return false;
        }
    }
}
