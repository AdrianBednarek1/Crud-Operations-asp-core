using Project.Service;
using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.Interfaces.IVehicleService;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleService : IVehicleService
    {
        private IVehicleRepository vehicleRepository;
        public VehicleService(IVehicleRepository _vehicleRepository)
        {
            vehicleRepository = _vehicleRepository;
        }
        public VehicleDB GetQueryDB()
        {
            return vehicleRepository.GetDBQuery();
        }
        public async Task<List<VehicleMake>> GetVehicleMakes()
        {
            return await vehicleRepository.GetVehicleMades();
        }
        public async Task<VehicleMake> SearchVehicleMake(int id)
        {
            return await vehicleRepository.SearchVehicleMake(id);
        }
        public async Task<VehicleModel> SearchVehicleModel(int id)
        {
            return await vehicleRepository.SearchVehicleModel(id);
        }
        public async Task<List<VehicleModel>> GetVehicleModels()
        {
            return await vehicleRepository.GetVehicleModels();
        }
        public async Task Create(object data)
        {
            if (TrueIfModel(data))
            {
                VehicleModel? model = data as VehicleModel;
                await vehicleRepository.CreateVehicleModel(model);
            }
            else
            {
                VehicleMake? made = data as VehicleMake;
                await vehicleRepository.CreateVehicleMade(made);
            }
        }
        public async Task Update(object data)
        {
            if (TrueIfModel(data))
            {
                VehicleModel? model = data as VehicleModel;
                await vehicleRepository.UpdateVehicleModel(model);
            }
            else
            {
                VehicleMake? made = data as VehicleMake;
                await vehicleRepository.UpdateVehicleMake(made);
            }
        }
        public async Task Delete(object data)
        {
            if (TrueIfModel(data))
            {
                VehicleModel? model = data as VehicleModel;
                await vehicleRepository.DeleteVehicleModel(model);
            }
            else
            {
                VehicleMake? made = data as VehicleMake;
                await vehicleRepository.DeleteVehicleMake(made);
            }
        }
        private bool TrueIfModel(object data)
        {
            if (data.GetType() == typeof(VehicleModel))
            {
                return true;
            }
            return false;
        }
        public async Task<bool> VehicleMakeIsNull()
        {
            return await vehicleRepository.VehicleMakesIsNull();
        }
        public async Task<bool> VehicleModelIsNull()
        {
            return await vehicleRepository.VehicleModelsIsNull();
        }
    }
}
