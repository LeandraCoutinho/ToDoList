using ToDoList.Core.Exceptions;
using ToDoList.Domain.Validators;

namespace ToDoList.Domain.Entities;

public class User : Base
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    
    public ICollection<Assignment> Assignments { get; set; } 
    public ICollection<AssignmentList> AssignmentLists { get; set; }    
    

    public User()
    { }

    public User(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
        Assignments = new List<Assignment>();
        AssignmentLists = new List<AssignmentList>();
        _errors = new List<string>();

        Validate();
    }

    public override bool Validate()
    {
        var validator = new UserValidator();
        var validation = validator.Validate(this);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
                _errors.Add(error.ErrorMessage);

            throw new DomainException("Alguns campos estão incorretos, por favor, corrijá-os!" + _errors);
        }

        return true;
    }
}