using AutoMapper;
using WebApplicationTraining5.DTO;
using WebApplicationTraining5.Entities;

namespace WebApplicationTraining5.Mapper
{
    public class UserDepartmentMapper : Profile
    {
        public UserDepartmentMapper() 
        {
            CreateMap<UserDepartments, UserDepartmentDTO>();
            CreateMap<UserDepartmentDTO, UserDepartments>();
        }
    }
}
