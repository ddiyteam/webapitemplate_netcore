using AutoMapper;

namespace $ext_safeprojectname$.API.Mappings
{
    public class CarsViewMappings : Profile
    {
        public CarsViewMappings()
        {
            CreateMap<BLL.Models.Car, Models.Car>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.ModelName, opt => opt.MapFrom(src => src.ModelName))               
                .ForMember(d => d.CarType, opt => opt.MapFrom(src => src.CarType))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(d => d.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn));

            CreateMap<Models.Car, BLL.Models.Car>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.ModelName, opt => opt.MapFrom(src => src.ModelName))                
                .ForMember(d => d.CarType, opt => opt.MapFrom(src => src.CarType))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(d => d.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn));
        }
    }
}
