using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Infra.Data.Mappings;

namespace ToDoList.Infra.Data.Context;

public class ToDoListContext : DbContext
{
    public ToDoListContext() { } 
    public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options) {}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        var connection = "Server=localhost;port=3306;Database=todolist;Uid=root;Pwd=12345";
        options.UseMySql(connection, ServerVersion.AutoDetect(connection));
    }
    
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<AssignmentList> AssignmentLists { get; set; } = null!;
    public DbSet<Assignment> Assignments { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}