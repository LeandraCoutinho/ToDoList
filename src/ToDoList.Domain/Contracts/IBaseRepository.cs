using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Contracts;

public interface IBaseRepository<T> where T : Base
{
    Task<T> Create(T obj);
    Task<T> Update(T obj);
    Task Remove(int id);
    Task<T?> Get(int id);
    Task<List<T>> Get();
}