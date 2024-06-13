using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Domain.Contracts;
using ToDoList.Infra.Data.Context;
using ToDoList.Infra.Data.Repositories;
using ToDoList.Infra.Data.Repository;

namespace ToDoList.Infra.Data;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        var serverVersion = new MySqlServerVersion(ServerVersion.AutoDetect(connectionString));

        services.AddDbContext<ToDoListContext>(options =>
            options.UseMySql(connectionString, serverVersion));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IAssignmentListRepository, AssignmentListRepository>();

        return services;
    }

    /*
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Domain));
        
    }
    */
}