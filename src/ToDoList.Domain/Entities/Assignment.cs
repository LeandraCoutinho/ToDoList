using ToDoList.Core.Exceptions;
using ToDoList.Domain.Validators;

namespace ToDoList.Domain.Entities;

public class Assignment : Base
{
    public string Description { get; set; } = null!;
    public int UserId { get; set; }
    public int? AssignmentListId { get; set; }
    public string Concluded { get; set; } = null!;
    public DateTime? ConcluedAt { get; set; }
    public DateTime? Deadline { get; set; }

    public User User { get; set; } = null!;
    public AssignmentList AssignmentList { get; set; } = null!;
    
    public Assignment()
    { }

    public Assignment(string description, int userId, string concluded)
    {
        Description = description;
        UserId = userId;
        Concluded = concluded;
        _errors = new List<string>();
    }
    
    public override bool Validate()
    {
        var validator = new AssignmentValidator();
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