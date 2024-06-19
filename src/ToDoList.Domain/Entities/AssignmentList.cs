using ToDoList.Core.Exceptions;
using ToDoList.Domain.Validators;

namespace ToDoList.Domain.Entities;

public class AssignmentList : Base
{
    public string Name { get; set; } = null!;
    public int UserId { get; set; } 

    public User User { get; set; } = null!;
    public ICollection<Assignment> Assignments { get; set; } 

    public AssignmentList()
    { }

    public AssignmentList(string name, int userId)
    {
        Name = name;
        UserId = userId;
        Assignments = new List<Assignment>();
        _errors = new List<string>();
    }

    public override bool Validate()
    {
        var validator = new AssignmentListValidator();
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