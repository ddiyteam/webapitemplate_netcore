using AutoMapper;
using $ext_safeprojectname$.BLL.Models;
using $ext_safeprojectname$.DAL.MySql.Models;
using System;

namespace $ext_safeprojectname$.BLL.Mappings
{
    public class CarsMapping: Profile
    {
        public CarsMapping()
        {
            CreateMap<Car, CarEntity>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(d => d.ModelName, opt => opt.MapFrom(src => src.ModelName))               
                .ForMember(d => d.CarType, opt => opt.MapFrom(src => src.CarType))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(d => d.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn));

            CreateMap<CarEntity, Car>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
                .ForMember(d => d.ModelName, opt => opt.MapFrom(src => src.ModelName))              
                .ForMember(d => d.CarType, opt => opt.MapFrom(src => src.CarType))
                .ForMember(d => d.CreatedOn, opt => opt.MapFrom(src => src.CreatedOn))
                .ForMember(d => d.ModifiedOn, opt => opt.MapFrom(src => src.ModifiedOn));
        }       
    }
}
