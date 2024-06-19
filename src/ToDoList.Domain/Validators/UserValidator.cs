using FluentValidation;
using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x)
            .NotEmpty()
            .WithMessage("A entidade não pode estar vazia.")

            .NotNull()
            .WithMessage("A entidade não pode ser nula");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("O nome não pode estar vazio")

            .NotNull()
            .WithMessage("O nome não pode ser nulo.")

            .MinimumLength(3)
            .WithMessage("O nome deve conter no mínimo 3 caracteres.")

            .MaximumLength(80)
            .WithMessage("O nome deve ter no máximo 80 caracteres");
        
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("O email não pode estar vazio")

            .NotNull()
            .WithMessage("O email não pode ser nulo.")

            .MinimumLength(10)
            .WithMessage("O email deve conter no mínimo 10 caracteres.")

            .MaximumLength(180)
            .WithMessage("O email deve ter no máximo 80 caracteres");

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("A senha não pode estar vazia")

            .NotNull()
            .WithMessage("A senha não pode ser nula.")

            .MinimumLength(6)
            .WithMessage("A senha deve conter no mínimo 6 caracteres.")

            .MaximumLength(255)
            .WithMessage("A senha deve ter no máximo 255 caracteres");
    }
}