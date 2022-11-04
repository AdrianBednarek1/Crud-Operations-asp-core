using AutoMapper;
using Project.Service.Interfaces.IVehicleRepository;
using Project.Service.PagingSortingFiltering;
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
        public async Task<IQueryable<VehicleModel>> GetDBQueryModel()
        {
            return vehicleDB.vehicleModels.AsQueryable();
        }
        public async Task<List<VehicleModel>> ReturnModelList()
        {
            IQueryable<VehicleModel> paginetedQuery;
            paginetedQuery = pagingModel.paginetedModelQuery ?? vehicleDB.vehicleModels.AsQueryable();

            IQueryable<VehicleModel> filteredQuery;
            filteredQuery = filteringModel.filteredModelQuery ?? vehicleDB.vehicleModels.AsQueryable();

            IQueryable<VehicleModel> filteredPaginetedQuery;
            filteredPaginetedQuery = paginetedQuery.Intersect(filteredQuery);

            List<VehicleModel> paginetedFilteredSortedList;
            paginetedFilteredSortedList = await filteredPaginetedQuery.ToListAsync();

            if (sortingModel.descending)
            {
                return paginetedFilteredSortedList.OrderByDescending(p => p.GetType().GetProperty(sortingModel.nameOfProperty).GetValue(p)).ToList();
            }
            return paginetedFilteredSortedList.OrderBy(p => p.GetType().GetProperty(sortingModel.nameOfProperty).GetValue(p)).ToList();
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
        public async Task<bool> VehicleMakeIsNull()
        {
            if (!await vehicleDB.vehicleMakes.AnyAsync() || vehicleDB.vehicleMakes == null)
            {
                return true;
            }
            return false;
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
        public async Task PagingVehicleModel(int pageIndex, int pageSize)
        {
            await pagingModel.CreateAsync(pageIndex, pageSize);
        }
        public async Task FilterVehicleModel(string searchStringModel, string currentSearchModel, int? pageIndexMake)
        {
            await filteringModel.FilterModel(searchStringModel, currentSearchModel, pageIndexMake);
        }
        public async Task<VehicleModel> SearchVehicleModel(int id)
        {
            var vehicleModel = await vehicleDB.vehicleModels.SingleOrDefaultAsync(d => d.Id == id);
            return vehicleModel;
        }
        public async Task SortVehicleModel(string sortOrderModel)
        {
            await sortingModel.SortModel(sortOrderModel);
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
