using ToDoList.Core.Exceptions;
using ToDoList.Domain.Validators;

namespace ToDoList.Domain.Entities;

public class AssignmentList : Base
{
    public string Name { get; private set; }
    public int UserId { get; private set; }

    public User User { get; set; }
    public ICollection<Assignment> Assignments { get; set; }

    public AssignmentList()
    { }

    public AssignmentList(string name, int userId)
    {
        Name = name;
        UserId = userId;
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