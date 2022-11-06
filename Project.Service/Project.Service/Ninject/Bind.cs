using Ninject.Modules;
using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmake;
using Project.Service.Interfaces.ISortingFilteringPaging.IPSFmodel;
using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.Interfaces.IVehicleService;
using Project.Service.PagingSortingFiltering;
using Project.Service.PagingSortingFiltering.PSFmake;
using Project.Service.PagingSortingFiltering.PSFmodel;
using Project.Service.VehicleService;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service
{
    public class Bind : NinjectModule
    {
        public override void Load()
        {
            Bind<IConfiguration>().To<ConfigurationManager>();
            Bind<IVehicleRepositoryMake>().To<VehicleRepositoryMake>();
			Bind<ISortingMake>().To<SortingMake>();
            Bind<IPagingMake>().To<PagingMake>();
            Bind<IFilteringMake>().To<FilteringMake>();

            Bind<IVehicleRepositoryModel>().To<VehicleRepositoryModel>();
            Bind<ISortingModel>().To<SortingModel>();
            Bind<IPagingModel>().To<PagingModel>();
            Bind<IFilteringModel>().To<FilteringModel>();
        }
    }
}
