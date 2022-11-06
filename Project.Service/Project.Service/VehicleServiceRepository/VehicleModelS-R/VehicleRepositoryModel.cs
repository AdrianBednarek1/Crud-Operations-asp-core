using AutoMapper;
using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.PagingSortingFiltering;
using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.PagingSortingFiltering.PSFmodel;
using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.VehicleService
{
    public class VehicleRepositoryModel : IVehicleRepositoryModel
    {
        private VehicleDB vehicleDB;
        public FilteringModel filteringModel { get; set; }
        public SortingModel sortingModel { get; set; }
        public PagingModel pagingModel { get; set; }
        public VehicleRepositoryModel()
        {
            vehicleDB = VehicleStaticDatabase.vehicleDB;
            filteringModel = new FilteringModel();
            sortingModel = new SortingModel();
            pagingModel = new PagingModel();
        }
        public async Task<List<VehicleModel>> GetVehicleModel()
        {
            return await vehicleDB.vehicleModels.ToListAsync();
        }
        public async Task<List<VehicleModel>> GetVehicleModel
            (SortParameters sortParameters, FilterParameters filterParameters, PageParameters pageParameters)
        {
            IQueryable<VehicleModel> pageQuery = await pagingModel.GetPageModel(pageParameters);
            IQueryable<VehicleModel> filterQuery = await filteringModel.GetFilterModel(filterParameters);

            string propertyNameSort = await sortingModel.GetPropertyNameSort(sortParameters);

            List<VehicleModel> intersectFilterPage = pageQuery.Intersect(filterQuery).ToList();
            List<VehicleModel> filterPageSort = GetSortList(intersectFilterPage, propertyNameSort);

            return filterPageSort;
        }
        private List<VehicleModel> GetSortList(List<VehicleModel> filterPage, string propertyNameSort)
        {
            if (sortingModel.isDescending)
            {
                return filterPage.OrderByDescending(p => p.GetType().GetProperty(propertyNameSort).GetValue(p)).ToList();
            }
            return filterPage.OrderBy(p => p.GetType().GetProperty(propertyNameSort).GetValue(p)).ToList();
        }
        public async Task Create(VehicleModel? createModel)
        {
            if (createModel != null)
            {
                vehicleDB.vehicleModels.Add(createModel);
                await vehicleDB.SaveChangesAsync();
            }
        }
        public async Task Delete(VehicleModel? deleteModel)
        {
            if (deleteModel != null)
            {
                vehicleDB.vehicleModels.Remove(deleteModel);
                await vehicleDB.SaveChangesAsync();
            }
        }
        public async Task Update(VehicleModel? updateModel)
        {
            if (updateModel != null)
            {
                var itemForUpdate = vehicleDB.vehicleModels.Single(d => d.Id == updateModel.Id);
                vehicleDB.Entry(itemForUpdate).CurrentValues.SetValues(updateModel);
                await vehicleDB.SaveChangesAsync();
            }
        }
        public async Task<VehicleModel> GetModelById(int id)
        {
            var vehicleModel = await vehicleDB.vehicleModels.SingleOrDefaultAsync(d => d.Id == id);
            return vehicleModel;
        }
    }
}
