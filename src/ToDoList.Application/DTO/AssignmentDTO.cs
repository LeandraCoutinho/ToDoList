namespace ToDoList.Application.DTO;

public class AssignmentDTO
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public int UserId { get; set; }
    public int? AssignmentListId { get; set; }
    
    public string Concluded { get;  set; }  = null!;
    public DateTime? ConcluedAt { get; set; }
    public DateTime? Deadline { get; set; }

    public AssignmentDTO()
    { }

    public AssignmentDTO(int id, string description, int userId, int? assignmentListId, string concluded, DateTime? concluedAt, DateTime? deadline)
    {
        Id = id;
        Description = description;
        UserId = userId;
        AssignmentListId = assignmentListId;
        Concluded = concluded;
        ConcluedAt = concluedAt;
        Deadline = deadline;
    }
}