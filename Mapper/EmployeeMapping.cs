using AutoMapper;
using WebApplicationTraining5.DTO;
using WebApplicationTraining5.Entities;
using WebApplicationTraining5.Enums;

namespace WebApplicationTraining5.Mapper
{
    public class EmployeeMapping : Profile
    {
        public EmployeeMapping()
        {
            CreateMap<Employee, EmployeeDTO>()
                .ForMember(emp => emp.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));

            CreateMap<EmployeeDTO, Employee>()
           .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => Enum.Parse<GenderEnum>(src.Gender)));
        }
    }
}
