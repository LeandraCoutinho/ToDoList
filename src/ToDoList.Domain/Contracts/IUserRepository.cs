using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Contracts;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User> GetByEmail(string email);
    Task<List<User>> SearchByEmail(string email);
}