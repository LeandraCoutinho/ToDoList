using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ToDoList.Domain.Contracts;
using ToDoList.Infra.Data.Context;
using ToDoList.Infra.Data.Repositories;

namespace ToDoList.Infra.Data;

public static class DependencyInjection
{
    public static void AddInfraestructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IAssignmentListRepository, AssignmentListRepository>();
    }

    /*
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Domain));
        
    }
    */
}