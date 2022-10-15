using Project.Service.Interfaces.IVehicleService;
using System.Data.Entity;
namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleRepository : IVehicleRepository
    {
        private VehicleDB vehicleDB;
        public VehicleRepository()
        {
            vehicleDB = new VehicleDB();
        }
        public VehicleDB GetDBQuery()
        {
            return vehicleDB;
        }
        public async Task<List<VehicleMake>> GetVehicleMades()
        {
            return await vehicleDB.vehicleMades.ToListAsync();
        }
        public async Task<List<VehicleModel>> GetVehicleModels()
        {
            return await vehicleDB.vehicleModels.ToListAsync();
        }
        public async Task CreateVehicleModel(VehicleModel? model)
        {
            if (model!=null)
            {
                vehicleDB.vehicleModels.Add(model);
                await vehicleDB.SaveChangesAsync();
            }         
        }
        public async Task CreateVehicleMade(VehicleMake? made)
        {
            if (made!=null)
            {
                vehicleDB.vehicleMades.Add(made);
            }
            await vehicleDB.SaveChangesAsync();
        }
        public async Task DeleteVehicleModel(VehicleModel? model)
        {
            if (model != null)
            {
                vehicleDB.vehicleModels.Remove(model);
            }
            await vehicleDB.SaveChangesAsync();
        }
        public async Task DeleteVehicleMade(VehicleMake? made)
        {
            if (made !=null)
            {
                vehicleDB.vehicleMades.Remove(made);
            }           
            await vehicleDB.SaveChangesAsync();
        }
        public async Task UpdateVehicleMade(VehicleMake? made)
        {
            if (made != null)
            {
                vehicleDB.vehicleMades.Single(d => d.Id == made.Id).Id = made.Id;
                vehicleDB.vehicleMades.Single(d => d.Id == made.Id).Abrv = made.Abrv;
                vehicleDB.vehicleMades.Single(d => d.Id == made.Id).Name = made.Name;
            }                   
            await vehicleDB.SaveChangesAsync();
        }
        public async Task UpdateVehicleModel(VehicleModel? model)
        {
            if (model!=null)
            {
                vehicleDB.vehicleModels.Single(d => d.Id == model.Id).Id = model.Id;
                vehicleDB.vehicleModels.Single(d => d.Id == model.Id).MakeId = model.MakeId;
                vehicleDB.vehicleModels.Single(d => d.Id == model.Id).Abrv = model.Abrv;
                vehicleDB.vehicleModels.Single(d => d.Id == model.Id).Name = model.Name;
            }
            
            await vehicleDB.SaveChangesAsync();
        }
        public async Task<VehicleMake> SearchVehicleMade(int id)
        {
            var vehicleMade = await vehicleDB.vehicleMades.SingleAsync(d => d.Id == id);
            return vehicleMade;
        }
    }
}
