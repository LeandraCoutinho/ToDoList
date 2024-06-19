using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Contracts;
using ToDoList.Domain.Entities;
using ToDoList.Infra.Data.Context;

namespace ToDoList.Infra.Data.Repositories;

public class AssignmentRepository : BaseRepository<Assignment>, IAssignmentRepository
{
    private readonly ToDoListContext _context;
    
    public AssignmentRepository(ToDoListContext context) : base(context)
    {
        _context = context;
    }

    public virtual async Task<Assignment> GetById(int id, int userId)
    {
        var assignment = await _context.Set<Assignment>().Where
            (
                x => x.Id == id && x.UserId == userId
            )
            .AsNoTracking()
            .ToListAsync();

        return assignment.FirstOrDefault();
    }

    public virtual async Task<Assignment> GetByDescription(string description)
    {
        var assignmentDescription = await _context.Set<Assignment>()
            .AsNoTracking()
            .Where(
                x => x.Description == description)
            .ToListAsync();

        return assignmentDescription.FirstOrDefault();
    }

    public virtual async Task<List<Assignment>> GetConcluded()
    {
        return await _context.Assignments.Where
            (p => p.Concluded == "true")
            .AsNoTracking()
            .ToListAsync();
    }
}