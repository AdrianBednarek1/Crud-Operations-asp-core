using Project.Service;
using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.Interfaces.IVehicleService;
using System.Data.Entity;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleServiceMake : IVehicleServiceMake
    {
        private IVehicleRepositoryMake vehicleRepository;
        public VehicleServiceMake(IVehicleRepositoryMake _vehicleRepository)
        {
            vehicleRepository = _vehicleRepository;
        }
        public DbSet<VehicleMake> GetQueryDBmake()
        {
            return vehicleRepository.GetDBQueryMake();
        }
        public async Task<List<VehicleMake>> GetVehicleMakes()
        {
            return await vehicleRepository.GetVehicleMakes();
        }
        public async Task<VehicleMake> SearchVehicleMake(int id)
        {
            return await vehicleRepository.SearchVehicleMake(id);
        }
        public async Task Create(VehicleMake data)
        {           
            VehicleMake? made = data;
            await vehicleRepository.CreateVehicleMake(made);           
        }
        public async Task Update(VehicleMake data)
        {
            VehicleMake? made = data;
            await vehicleRepository.UpdateVehicleMake(made);
        }
        public async Task Delete(VehicleMake data)
        {
            VehicleMake? made = data;
            await vehicleRepository.DeleteVehicleMake(made);            
        }
        public async Task<bool> VehicleMakeIsNull()
        {
            return await vehicleRepository.VehicleMakesIsNull();
        }
    }
}
