using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Contracts;
using ToDoList.Domain.Entities;
using ToDoList.Infra.Data.Context;

namespace ToDoList.Infra.Data.Repository;

public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
{
    private readonly ToDoListContext _context;
    
    public AssignmentRepository(ToDoListContext context) : base(context)
    {
        _context = context;
    }

    public virtual async Task<List<Assignment>> GetById(int id, int userId)
    {
        return await _context.Assignments.Where
            (
                x => x.Id == id && x.UserId == userId
            )
            .AsNoTracking()
            .ToListAsync();
    }
}