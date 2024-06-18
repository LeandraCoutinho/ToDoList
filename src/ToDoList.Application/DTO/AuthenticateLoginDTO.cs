namespace ToDoList.Application.DTO;

public class AuthenticateLoginDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}