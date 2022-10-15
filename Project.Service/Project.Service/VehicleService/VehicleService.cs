using Project.Service;

namespace ZaPrav.NetCore.VehicleDB
{
    public static class VehicleService
    {
        private static VehicleRepository VehicleRepository = new VehicleRepository();
        public static VehicleDB GetQueryDB()
        {
            return VehicleRepository.GetDBQuery();
        }
        public static async Task<List<VehicleMake>> GetVehicleMades()
        {
            return await VehicleRepository.GetVehicleMades();
        }
        public static async Task<VehicleMake> SearchVehicleMade(int id)
        {          
            return await VehicleRepository.SearchVehicleMade(id);
        }
        public static async Task<List<VehicleModel>> GetVehicleModels()
        {
            return await VehicleRepository.GetVehicleModels();
        }
        public static async Task Create(object data)
        {
            if(TrueIfModel(data))
            {
                VehicleModel? model = data as VehicleModel;
                await VehicleRepository.CreateVehicleModel(model);
            }
            else
            {
                VehicleMake? made = data as VehicleMake;
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
                VehicleMake? made = data as VehicleMake;
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
                VehicleMake? made = data as VehicleMake;
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
