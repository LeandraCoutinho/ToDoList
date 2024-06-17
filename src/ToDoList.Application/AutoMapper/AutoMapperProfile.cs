using AutoMapper;
using ToDoList.Application.DTO;
using ToDoList.Domain.Entities;
using ToDoList.API.ViewModels.UserViewModel;

namespace ToDoList.Application.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
        CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
        
        CreateMap<Assignment, AssignmentDTO>().ReverseMap();
        
        CreateMap<AssignmentList, AssignmentListDTO>().ReverseMap();
    }
}