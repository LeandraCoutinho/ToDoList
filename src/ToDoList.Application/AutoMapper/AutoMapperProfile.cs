using AutoMapper;
using ToDoList.Application.DTO;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        
        CreateMap<Assignment, AssignmentDTO>().ReverseMap();
        
        CreateMap<AssignmentList, AssignmentListDTO>().ReverseMap();
    }
}