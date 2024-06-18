using ToDoList.Application.DTO;

namespace ToDoList.Application.Interfaces;

public interface IAuthenticateUser
{
    Task<AuthenticateLoginDTO> Authenticate(LoginDTO loginDto);
}