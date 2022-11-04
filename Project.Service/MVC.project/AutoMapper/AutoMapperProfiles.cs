using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC.project.ViewModels.MakeViewModels;
using MVC.project.ViewModels.ModelViewModels;
using ZaPrav.NetCore.VehicleDB;

namespace MVC.project.AutoMapper
{
    public class AutoMapperProfiles : Profile
    {
        public static IMapper mapper;
        public AutoMapperProfiles()
        {
            CreateMap<ModelViewModel, VehicleModel>().ForMember(d => d.Make, r => r.Ignore());
            CreateMap<ModelViewModel, VehicleModel>().ReverseMap();
            
            CreateMap<VehicleMake, MakeViewModel>();
            CreateMap<VehicleMake, MakeViewModel>().
                ReverseMap().ForMember(d=>d.Models, r=>r.Ignore()).ForAllMembers(opt=> opt.Condition(r=> r!=null));
        }
    }
}
