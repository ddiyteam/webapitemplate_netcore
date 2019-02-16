using AutoMapper;

namespace $ext_safeprojectname$.API.Mappings
{
    public class TodoViewMappings : Profile
    {
        public TodoViewMappings()
        {
            CreateMap<BLL.Models.Todo, Models.Todo>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(d => d.Completed, opt => opt.MapFrom(src => src.Completed));               

            CreateMap<Models.Todo, BLL.Models.Todo>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(d => d.Title, opt => opt.MapFrom(src => src.Title))
                .ForMember(d => d.Completed, opt => opt.MapFrom(src => src.Completed));
        }
    }
}
