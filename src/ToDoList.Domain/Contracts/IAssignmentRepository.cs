using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Contracts;

public interface IAssignmentRepository : IBaseRepository<Assignment>
{
    Task<Assignment> GetById(int id, int userId);
    Task<Assignment> GetByDescription(string description);
    Task<List<Assignment>> GetConcluded();
}