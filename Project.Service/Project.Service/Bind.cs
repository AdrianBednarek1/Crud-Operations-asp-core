using Ninject.Modules;
using System.Collections.ObjectModel;
using System.Linq;
using ZaPrav.NetCore;
using ZaPrav.NetCore.Interfaces;
using ZaPrav.NetCore.Interfaces.IPages.IIndex;
using ZaPrav.NetCore.Interfaces.IPages.IMadeCreator;
using ZaPrav.NetCore.Interfaces.ISortingHelp;
using ZaPrav.NetCore.Interfaces.IUpdateVehicleModels;
using ZaPrav.NetCore.Interfaces.IVehicleDB;
using ZaPrav.NetCore.Pages;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service
{
    public class Bind : NinjectModule
    {
        public override void Load()
        {
            //Bind < IList<IVehicleMade>().To<List<VehicleMVeade>>().InSingleScope();
            Bind<IVehicleMake>().To<VehicleMake>();
            Bind<IVehicleModel>().To<VehicleModel>();
            Bind<IVehicleDB>().To<VehicleDB>();
            Bind<ISortingHelp>().To<SortingHelp>();
            Bind<IMadeCreator>().To<MadeCreator>();
            Bind<IIndexModel>().To<IndexModel>();
            Bind<IModelCreator>().To<ModelCreatorModel>();
            Bind<IUpdateVehicleModels>().To<UpdateVehicleModelsModel>();
            Bind<IUpdateVehicleModels>().To<UpdateVehicleModelsModel>();
            
        }
    }
}
