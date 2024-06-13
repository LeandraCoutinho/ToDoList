using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Contracts;

public interface IAssignmentListRepository : IBaseRepository<AssignmentList>
{
    Task<AssignmentList> GetByName(string name);
    Task<List<AssignmentList>> SearchByName(string name);
}