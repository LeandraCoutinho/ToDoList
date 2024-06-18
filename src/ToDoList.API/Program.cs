using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDoList.API.ViewModels.AssignmentListVM;
using ToDoList.API.ViewModels.AssignmentVM;
using ToDoList.API.ViewModels.UserVM;
using ToDoList.Application.DTO;
using ToDoList.Domain.Entities;
using ToDoList.Infra.Data;
using ToDoList.Infra.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// injeção de dependência
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();

AutoMapperDependenceInjection();

void AutoMapperDependenceInjection()
{
    var autoMapperConfig = new MapperConfiguration(cfg =>
    {
        cfg.CreateMap<User, UserDTO>().ReverseMap();
        cfg.CreateMap<CreateUserViewModel, UserDTO>().ReverseMap();
        cfg.CreateMap<UpdateUserViewModel, UserDTO>().ReverseMap();
        
        cfg.CreateMap<Assignment, AssignmentDTO>().ReverseMap();
        cfg.CreateMap<CreateAssignmentViewModel, AssignmentDTO>().ReverseMap();
        cfg.CreateMap<UpdateAssignmentViewModel, AssignmentDTO>().ReverseMap();

        cfg.CreateMap<AssignmentList, AssignmentListDTO>().ReverseMap();
        cfg.CreateMap<CreateAssignmentListViewModel, AssignmentListDTO>().ReverseMap();
        cfg.CreateMap<UpdateAssignmentListViewModel, AssignmentListDTO>().ReverseMap();
    });
    builder.Services.AddSingleton(autoMapperConfig.CreateMapper());
}

builder.Services.AddSingleton(d => builder.Configuration);

builder.Services.AddDbContext<ToDoListContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();