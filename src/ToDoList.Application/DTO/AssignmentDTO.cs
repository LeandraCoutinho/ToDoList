namespace ToDoList.Application.DTO;

public class AssignmentDTO
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int UserId { get; set; }
    public int? AssignmentListId { get; set; }
    public bool Conclued { get; set; }
    public DateTime? ConcluedAt { get; set; }
    public DateTime? Deadline { get; set; }

    public AssignmentDTO()
    { }

    public AssignmentDTO(int id, string description, int userId, int? assignmentListId, bool conclued, DateTime? concluedAt, DateTime? deadline)
    {
        Id = id;
        Description = description;
        UserId = userId;
        AssignmentListId = assignmentListId;
        Conclued = conclued;
        ConcluedAt = concluedAt;
        Deadline = deadline;
    }
}