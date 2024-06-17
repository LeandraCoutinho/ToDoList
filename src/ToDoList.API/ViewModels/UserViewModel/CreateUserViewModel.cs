using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.ViewModels.UserViewModel;

public class CreateUserViewModel
{
    [Required(ErrorMessage = "O nome não pode ser nulo.")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracterers.")]
    [MaxLength(80, ErrorMessage = "O nome deve ter no máximo 80 caracteres.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "O email não pode ser nulo.")]
    [MinLength(10, ErrorMessage = "O email deve ter no mínimo 3 caracterers.")]
    [MaxLength(180, ErrorMessage = "O email deve ter no máximo 180 caracteres.")]
    public string Email { get; set; } = null!;
    
    [Required(ErrorMessage = "A senha não pode ser nula.")]
    [MinLength(6, ErrorMessage = "A senha deve ter no mínimo 6 caracteres.")]
    [MaxLength(30, ErrorMessage = "A senha deve ter no máximo 30 caracteres")]
    public string Password { get; set; } = null!;
}