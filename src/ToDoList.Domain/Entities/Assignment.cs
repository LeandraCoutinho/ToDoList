using ToDoList.Domain.Validators;

namespace ToDoList.Domain.Entities;

public class Assignment : Base
{
    public string Description { get; set; }
    public int UserId { get; set; }
    public int? AssignmentListId { get; set; }
    public bool Concluded { get; set; }
    public DateTime? ConcludedAt { get; set; }
    public DateTime? Deadline { get; set; }
    

    public Assignment()
    { }

    public Assignment(string description, int userId, int? assignmentListId, bool concluded, DateTime? concludedAt, DateTime? deadline)
    {
        Description = description;
        UserId = userId;
        AssignmentListId = assignmentListId;
        Concluded = concluded;
        ConcludedAt = concludedAt;
        Deadline = deadline;
    }

    public override bool Validate()
    {
        var validator = new AssignmentValidator();
        var validation = validator.Validate(this);

        if (!validation.IsValid)
        {
            foreach (var error in validation.Errors)
                _errors.Add(error.ErrorMessage);

            throw new Exception("Alguns campos estão incorretos, por favor, corrijá-os!" + _errors);
        }

        return true;
    }
}