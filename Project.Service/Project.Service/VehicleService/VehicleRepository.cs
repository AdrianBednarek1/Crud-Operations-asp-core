using System.Data.Entity;
namespace ZaPrav.NetCore.VehicleDB
{
    public static class VehicleRepository
    {
        private static VehicleDB vehicleDB = new VehicleDB();
        public static VehicleDB GetDBQuery()
        {
            return vehicleDB;
        }
        public static async Task<List<VehicleMade>> GetVehicleMades()
        {
            return await vehicleDB.vehicleMades.ToListAsync();
        }
        public static async Task<List<VehicleModel>> GetVehicleModels()
        {
            return await vehicleDB.vehicleModels.ToListAsync();
        }

        public static async Task CreateVehicleModel(VehicleModel model)
        {
            vehicleDB.vehicleModels.Add(model);
            await vehicleDB.SaveChangesAsync();
        }
        public static async Task CreateVehicleMade(VehicleMade made)
        {
            vehicleDB.vehicleMades.Add(made);
            await vehicleDB.SaveChangesAsync();
        }
        public static async Task DeleteVehicleModel(VehicleModel model)
        {
            vehicleDB.vehicleModels.Remove(model);
            await vehicleDB.SaveChangesAsync();
        }
        public static async Task DeleteVehicleMade(VehicleMade made)
        {                       
            vehicleDB.vehicleMades.Remove(made);
            await vehicleDB.SaveChangesAsync();
        }
        public static async Task UpdateVehicleMade(VehicleMade made)
        {
                   
            vehicleDB.vehicleMades.Single(d => d.Id == made.Id).Id = made.Id;
            vehicleDB.vehicleMades.Single(d => d.Id == made.Id).Abrv = made.Abrv;
            vehicleDB.vehicleMades.Single(d => d.Id == made.Id).Name = made.Name;
           
            await vehicleDB.SaveChangesAsync();
        }
        public static async Task UpdateVehicleModel(VehicleModel model)
        {
            vehicleDB.vehicleModels.Single(d => d.Id == model.Id).Id = model.Id;
            vehicleDB.vehicleModels.Single(d => d.Id == model.Id).IdMade = model.IdMade;
            vehicleDB.vehicleModels.Single(d => d.Id == model.Id).Abrv = model.Abrv;
            vehicleDB.vehicleModels.Single(d => d.Id == model.Id).Name = model.Name;

            await vehicleDB.SaveChangesAsync();
        }
        public static async Task<VehicleMade> SearchVehicleMade(int id)
        {
            var vehicleMade = await vehicleDB.vehicleMades.SingleAsync(d => d.Id == id);
            return vehicleMade;
        }
        
    }
}
