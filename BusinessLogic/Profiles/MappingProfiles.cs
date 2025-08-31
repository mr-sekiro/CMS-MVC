using AutoMapper;
using BusinessLogic.Data_Transfer_Object.EmployeeDtos;
using DataAccess.Models.EmployeeModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType));

            CreateMap<Employee, EmployeeDetailsDto>()
                .ForMember(dest => dest.Gender, options => options.MapFrom(src => src.Gender))
                .ForMember(dest => dest.EmployeeType, options => options.MapFrom(src => src.EmployeeType))
                .ForMember(dest => dest.HiringDate, options => options.MapFrom(src => DateOnly.FromDateTime(src.HhiringDate)));

            CreateMap<UpdatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HhiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)))
                .ReverseMap();
            CreateMap<CreatedEmployeeDto, Employee>()
                .ForMember(dest => dest.HhiringDate, options => options.MapFrom(src => src.HiringDate.ToDateTime(TimeOnly.MinValue)))
                .ReverseMap();
        }
    }
}
