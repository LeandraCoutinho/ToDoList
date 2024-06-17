using ToDoList.Application.DTO;

namespace ToDoList.Application.Interfaces;

public interface IAssignmentService
{
    Task<AssignmentDTO> Create(AssignmentDTO assignmentDto);
    Task<AssignmentDTO> Update(AssignmentDTO assignmentDto);
    Task Remove(int id);
    Task<AssignmentDTO> Get(int id);
    Task<List<AssignmentDTO>> GelAll();
    Task<List<AssignmentDTO>> GetConcluded();
    Task<AssignmentDTO> GetByDescription(string description);
}