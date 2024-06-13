using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Contracts;
using ToDoList.Domain.Entities;
using ToDoList.Infra.Data.Context;

namespace ToDoList.Infra.Data.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : Base
{
    private readonly ToDoListContext _context;

    public BaseRepository(ToDoListContext context)
    {
        _context = context;
    }

    public virtual async Task<T> Create(T obj)
    {
        _context.Add(obj);
        await _context.SaveChangesAsync();

        return obj;
    }

    public virtual async Task<T> Update(T obj)
    {
        _context.Entry(obj).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return obj;
    }

    public virtual async Task Remove(int id)
    {
        var obj = await Get(id);

        _context.Remove(obj);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<T?> Get(int id)
    {
        var obj = await _context.Set<T>()
            .AsNoTracking()
            .Where(x => x.Id == id)
            .ToListAsync();

        return obj.FirstOrDefault();
    }

    public virtual async Task<List<T>> Get()
    {
        return await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();
    }
}