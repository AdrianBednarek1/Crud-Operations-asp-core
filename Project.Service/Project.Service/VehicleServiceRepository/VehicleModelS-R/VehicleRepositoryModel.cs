using AutoMapper;
using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.PagingSortingFiltering;
using Project.Service.PagingSortingFiltering.Parameters;
using Project.Service.PagingSortingFiltering.PSFmodel;
using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.VehicleService
{
    public class VehicleRepositoryModel// : IVehicleRepositoryModel
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
            IQueryable<VehicleModel> pageQuery;
            pageQuery = await pagingModel.GetPageModel(pageParameters);

            IQueryable<VehicleModel> filterQuery;
            filterQuery = await filteringModel.GetFilterModel(filterParameters);

            string propertyNameSort = await sortingModel.SortModel(sortParameters);

            List<VehicleModel> intersectFilterPage;
            intersectFilterPage = pageQuery.Intersect(filterQuery).ToList();

            List<VehicleModel> filterPageSort;
            filterPageSort = SetOrderBy(intersectFilterPage, propertyNameSort);

            return filterPageSort;
        }
        private List<VehicleModel> SetOrderBy(List<VehicleModel> filterPage, string propertyNameSort)
        {
            if (sortingModel.isDescending)
            {
                return filterPage.OrderByDescending(p => p.GetType().GetProperty(propertyNameSort).GetValue(p)).ToList();
            }
            return filterPage.OrderBy(p => p.GetType().GetProperty(propertyNameSort).GetValue(p)).ToList();
        }
        public async Task Create(VehicleModel? model)
        {
            if (model != null)
            {
                vehicleDB.vehicleModels.Add(model);
                await vehicleDB.SaveChangesAsync();
            }
        }
        public async Task Delete(VehicleModel? model)
        {
            if (model != null)
            {
                vehicleDB.vehicleModels.Remove(model);
                await vehicleDB.SaveChangesAsync();
            }
        }
        public async Task Update(VehicleModel? model)
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
        public async Task<VehicleModel> GetModelById(int id)
        {
            var vehicleModel = await vehicleDB.vehicleModels.SingleOrDefaultAsync(d => d.Id == id);
            return vehicleModel;
        }
    }
}
