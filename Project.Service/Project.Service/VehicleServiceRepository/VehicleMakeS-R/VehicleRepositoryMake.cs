using AutoMapper;
using Project.Service.Interfaces.IVehicleService;
using Project.Service.VehicleService;
using System.Data.Entity;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleRepositoryMake : IVehicleRepositoryMake
    {
        private VehicleDB vehicleDB;
        public VehicleRepositoryMake()
        {
            vehicleDB = VehicleStaticDatabase.vehicleDB;
        }
        public async Task<DbSet<VehicleMake>> GetDBQueryMake()
        {
            return vehicleDB.vehicleMakes;
        }
        public async Task<List<VehicleMake>> GetVehicleMakes()
        {
            return await vehicleDB.vehicleMakes.ToListAsync();
        }
        public async Task CreateVehicleMake(VehicleMake? made)
        {
            if (made!=null)
            {
                vehicleDB.vehicleMakes.Add(made);
            }
            await vehicleDB.SaveChangesAsync();
        }
        public async Task DeleteVehicleMake(VehicleMake? made)
        {
            if (made !=null)
            {
                foreach (var item in vehicleDB.vehicleModels)
                {
                    if(item.MakeId == made.Id && item!=null)
                    {
                        vehicleDB.vehicleModels.Remove(item);
                    }
                }
                vehicleDB.vehicleMakes.Remove(made);
            }           
            await vehicleDB.SaveChangesAsync();
        }
        public async Task UpdateVehicleMake(VehicleMake? made)
        {
            if (made != null)
            {
                vehicleDB.vehicleMakes.Single(d => d.Id == made.Id).Id = made.Id;
                vehicleDB.vehicleMakes.Single(d => d.Id == made.Id).Abrv = made.Abrv;
                vehicleDB.vehicleMakes.Single(d => d.Id == made.Id).Name = made.Name;
            }                   
            await vehicleDB.SaveChangesAsync();
        }
        public async Task<VehicleMake> SearchVehicleMake(int id)
        {
            var vehicleMade = await vehicleDB.vehicleMakes.SingleOrDefaultAsync(d => d.Id == id);
            return vehicleMade;
        }
        public async Task<bool> VehicleMakesIsNull()
        {
            if (!await vehicleDB.vehicleMakes.AnyAsync() || vehicleDB.vehicleMakes==null)
            {
                return true;
            }
            return false;
        }
    }
}
