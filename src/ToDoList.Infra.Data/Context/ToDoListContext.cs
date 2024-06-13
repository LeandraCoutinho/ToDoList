using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Infra.Data.Mappings;

namespace ToDoList.Infra.Data.Context;

public class ToDoListContext : DbContext
{
    public ToDoListContext() { }
    
    public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options) {}

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<AssignmentList> AssignmentLists { get; set; }
    public virtual DbSet<Assignment> Assignments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserMap());
        builder.ApplyConfiguration(new AssignmentMap());
        builder.ApplyConfiguration(new AssignmentListMap());
    }
}