using AutoMapper;
using Ninject.Modules;
using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.Interfaces.IVehicleService;
using Project.Service.VehicleService;
using System.Collections.ObjectModel;
using System.Linq;
using ZaPrav.NetCore;
using ZaPrav.NetCore.Interfaces;
using ZaPrav.NetCore.Interfaces.IPages.IIndex;
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
            Bind<IMapper>().To<IMapper>();
            Bind<IConfiguration>().To<ConfigurationManager>();
            Bind<IVehicleRepositoryMake>().To<VehicleRepositoryMake>();
            Bind<IVehicleServiceMake>().To<VehicleServiceMake>();
            Bind<IVehicleServiceModel>().To<VehicleServiceModel>();
            Bind<IVehicleRepositoryModel>().To<VehicleRepositoryModel>();


        }
    }
}
