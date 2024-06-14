using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Contracts;
using ToDoList.Domain.Entities;
using ToDoList.Infra.Data.Context;

namespace ToDoList.Infra.Data.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    private readonly ToDoListContext _context;
    
    public UserRepository(ToDoListContext context) : base(context)
    {
        _context = context;
    }

    public virtual async Task<User> GetByEmail(string email)
    {
        var user = await _context.Users.Where
            (
                x => x.Email.ToLower() == email.ToLower()
            )
            .AsNoTracking()
            .ToListAsync();

        return user.FirstOrDefault();
    }

    public virtual async Task<List<User>> SearchByEmail(string email)
    {
        var allUsers = await _context.Users.Where
            (
                x => x.Email.ToLower().Contains(email.ToLower())
            )
            .AsNoTracking()
            .ToListAsync();

        return allUsers;
    }
}