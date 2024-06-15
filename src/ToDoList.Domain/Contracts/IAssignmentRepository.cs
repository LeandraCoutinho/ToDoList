using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Contracts;

public interface IAssignmentRepository : IBaseRepository<Assignment>
{
    Task<List<Assignment>> GetById(int id, int userId);
    Task<List<Assignment>> GetConcluded();
}