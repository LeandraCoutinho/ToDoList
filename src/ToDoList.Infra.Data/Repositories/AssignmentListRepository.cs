using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Contracts;
using ToDoList.Domain.Entities;
using ToDoList.Infra.Data.Context;

namespace ToDoList.Infra.Data.Repositories;

public class AssignmentListRepository : BaseRepository<AssignmentList>, IAssignmentListRepository
{
    private readonly ToDoListContext _context;
    
    public AssignmentListRepository(ToDoListContext context) : base(context)
    {
        _context = context;
    }

    public virtual async Task<AssignmentList> GetByName(string name)
    {
        var assigmentList = await _context.AssignmentLists.Where
            (
                x => x.Name.ToLower().Contains(name.ToLower())
            )
            .AsNoTracking()
            .ToListAsync();

        return assigmentList.FirstOrDefault();
    }

    public virtual async Task<List<AssignmentList>> SearchByName(string name)
    {
        var allAssigmentLists = await _context.AssignmentLists.Where
            (
                x => x.Name.ToLower().Contains(name.ToLower())
            )
            .AsNoTracking()
            .ToListAsync();

        return allAssigmentLists;
    }
}