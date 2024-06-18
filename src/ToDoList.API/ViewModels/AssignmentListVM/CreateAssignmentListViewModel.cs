using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.ViewModels.AssignmentListVM;

public class CreateAssignmentListViewModel
{
    [Required(ErrorMessage = "O nome não pode ser nulo.")]
    [MinLength(3, ErrorMessage = "O nome deve ter no mínimo 3 caracterers.")]
    [MaxLength(20, ErrorMessage = "O nome deve ter no máximo 20 caracteres.")]
    public string Name { get; set; } = null!;

    [Required(ErrorMessage = "O Id do usuário não pode ser nulo.")]
    public int UserId { get; set; }
}