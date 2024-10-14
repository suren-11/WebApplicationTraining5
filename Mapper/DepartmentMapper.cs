using AutoMapper;
using WebApplicationTraining5.DTO;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.Mapper
{
    public class DepartmentMapper : Profile
    {
        public DepartmentMapper() 
        {
            CreateMap<Departmant, DepartmentDTO>();
            CreateMap<DepartmentDTO, Departmant>();
        }
    }
}
