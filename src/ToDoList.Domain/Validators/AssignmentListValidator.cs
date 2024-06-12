using FluentValidation;
using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Validators;

public class AssignmentListValidator : AbstractValidator<AssignmentList>
{
    public AssignmentListValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("A entidade não pode estar vazia.")
            
            .NotNull()
            .WithMessage("A entidade não pode ser nula");
        
        RuleFor(x => x.Name)
            .NotNull()
            .WithMessage("O nome não pode ser nulo.")

            .NotEmpty()
            .WithMessage("O nome não pode ser vazio.")
            
            .MinimumLength(3)
            .WithMessage("O nome deve ter no mínimo 3 caracteres.")

            .MaximumLength(20)
            .WithMessage("O nome deve ter no máximo 20 caracteres.");

        RuleFor(x => x.UserId)
            .NotNull()
            .WithMessage("O Id do usuário não pode ser nulo.")

            .NotEmpty()
            .WithMessage("O Id do usuário não pode ser vazio.");
    }
}