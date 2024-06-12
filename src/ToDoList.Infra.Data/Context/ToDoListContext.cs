using Microsoft.EntityFrameworkCore;

namespace ToDoList.Infra.Data.Context;

public class ToDoListContext : DbContext
{
    public ToDoListContext() { }
    
    public ToDoListContext(DbContextOptions<ToDoListContext> options) : base(options) {}
    
    
}