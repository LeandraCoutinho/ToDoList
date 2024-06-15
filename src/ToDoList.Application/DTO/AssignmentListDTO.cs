namespace ToDoList.Application.DTO;

public class AssignmentListDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int UserId { get; set; }

    public AssignmentListDTO()
    { }

    public AssignmentListDTO(int id, string name, int userId)
    {
        Id = id;
        Name = name;
        UserId = userId;
    }
}