using ToDoList.Core.Exceptions;
using ToDoList.Domain.Validators;

namespace ToDoList.Domain.Entities;

public class Assignment : Base
{
    public string Description { get; set; }
    public int UserId { get; set; }
    public int? AssignmentListId { get; set; }
    public bool Concluded { get; private set; }
    public DateTime? ConcluedAt { get; private set; }
    public DateTime? Deadline { get; set; }

    public User User { get; set; }
    public AssignmentList AssignmentList { get; set; }
    
    public Assignment()
    { }

    public Assignment(string description, int userId, int? assignmentListId, bool concluded, DateTime? concludedAt, DateTime? deadline)
    {
        Description = description;
        UserId = userId;
        AssignmentListId = assignmentListId;
        Concluded = concluded;
        ConcluedAt = concludedAt;
        Deadline = deadline;
    }

    public void SetConclud()
    {
        Concluded = true;
        ConcluedAt = DateTime.Now;
    }

    public void SetUnconclued()
    {
        Concluded = true;
        ConcluedAt = null;
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