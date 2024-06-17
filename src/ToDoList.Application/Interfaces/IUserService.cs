using ToDoList.Application.DTO;

namespace ToDoList.Application.Interfaces;

public interface IUserService
{
    Task<UserDTO> Create(UserDTO userDto);
    Task<UserDTO> Update(UserDTO userDto);
    Task Remove(int id);
    Task<UserDTO> Get(int id);
    Task<List<UserDTO>> GetAll();
    Task<UserDTO> GetByEmail(string email);
    Task<List<UserDTO>> SearchByEmail(string email);
}