using System.Data.Entity;
using ZaPrav.NetCore.Interfaces;
using ZaPrav.NetCore.Interfaces.IVehicleDB;

namespace ZaPrav.NetCore.VehicleDB
{
    public class VehicleDB : DbContext, IVehicleDB
    {
        public DbSet<VehicleMake> vehicleMades { get; set; }
        public DbSet<VehicleModel> vehicleModels { get; set; }
        //protected override void onmodelcreating(dbmodelbuilder modelbuilder)
        //{
        //    modelbuilder.entity<vehiclemake>().hasoptional(a => a.idmodel).hasrequired(d => d.user)
        //    .withoptional(u => u.userdetail)
        //    .willcascadeondelete(true);
        //    base.onmodelcreating(modelbuilder);
        //}
    }
}
