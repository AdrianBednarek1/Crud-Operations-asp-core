using AutoMapper;
using AutoMapper.EquivalencyExpression;
using System.Data.Entity;
using ZaPrav.NetCore.VehicleDB;

namespace Project.Service.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public static IMapper mapper;
        public AutoMapperProfiles() 
        {
            CreateMap<VehicleModel, InputData>();
            CreateMap<VehicleModel, VehicleModel>();
        }
    }
}
