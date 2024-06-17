using System.ComponentModel.DataAnnotations;

namespace ToDoList.API.ViewModels.AssignmentViewModel;

public class UpdateAssignmentViewModel
{
    [Required(ErrorMessage = "O id não pode ser vazio.")]
    [Range(1, int.MaxValue, ErrorMessage = "O id não pode ser manor que 1")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "A descrição não pode ser nula.")]
    [MinLength(5, ErrorMessage = "A descrição deve ter no mínimo 5 caracterers.")]
    [MaxLength(100, ErrorMessage = "A descrição deve ter no máximo 100 caracteres.")]
    public string Description { get; set; } = null!;
    
    [Required(ErrorMessage = "O Id do usuário não pode ser nulo.")]
    public int UserId { get; set; }
    
    [Required(ErrorMessage = "Informe se a tarefa já foi concluída.")]
    public bool Concluded { get; private set; }
    
    public int? AssignmentListId { get; set; }
    public DateTime? ConcluedAt { get; private set; }
    public DateTime? Deadline { get; set; }
}