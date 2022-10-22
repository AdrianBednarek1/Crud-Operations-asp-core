using AutoMapper;
using Project.Service.Interfaces.IVehicleRepository;
using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.VehicleService
{
    public class VehicleRepositoryModel : IVehicleRepositoryModel
    {
        private VehicleDB vehicleDB;
        public VehicleRepositoryModel()
        {
            vehicleDB = VehicleStaticDatabase.vehicleDB;
        }
        public DbSet<VehicleModel> GetDBQueryModel()
        {
            return vehicleDB.vehicleModels;
        }
        public async Task<List<VehicleModel>> GetVehicleModels()
        {
            return await vehicleDB.vehicleModels.ToListAsync();
        }
        public async Task CreateVehicleModel(VehicleModel? model)
        {
            if (model != null)
            {
                vehicleDB.vehicleModels.Add(model);
                await vehicleDB.SaveChangesAsync();
            }
        }
        public async Task DeleteVehicleModel(VehicleModel? model)
        {
            if (model != null)
            {
                vehicleDB.vehicleModels.Remove(model);
            }
            await vehicleDB.SaveChangesAsync();
        }
        public void DeleteVehicleModelWithoutSaving(VehicleModel? model)
        {
            if (model != null)
            {
                vehicleDB.vehicleModels.Remove(model);
            }
        }
        public async Task UpdateVehicleModel(VehicleModel? model)
        {
            if (model != null)
            {
                vehicleDB.vehicleModels.Single(d => d.Id == model.Id).Id = model.Id;
                vehicleDB.vehicleModels.Single(d => d.Id == model.Id).MakeId = model.MakeId;
                vehicleDB.vehicleModels.Single(d => d.Id == model.Id).Abrv = model.Abrv;
                vehicleDB.vehicleModels.Single(d => d.Id == model.Id).Name = model.Name;
            }
            await vehicleDB.SaveChangesAsync();
        }
        public async Task<VehicleModel> SearchVehicleModel(int id)
        {
            var vehicleModel = await vehicleDB.vehicleModels.SingleAsync(d => d.Id == id);
            return vehicleModel;
        }
        public async Task<bool> VehicleModelsIsNull()
        {
            if (!await vehicleDB.vehicleModels.AnyAsync() || vehicleDB.vehicleModels == null)
            {
                return true;
            }
            return false;
        }
    }
}
