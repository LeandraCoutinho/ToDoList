using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.ViewModels;

public class LoginViewModel
{
    [Required(ErrorMessage = "O login não pode ser vazio.")]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = "A senha não pode ser vazia")]
    public string Password { get; set; } = null!;
}