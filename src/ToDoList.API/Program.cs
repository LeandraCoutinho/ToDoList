using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ScottBrady91.AspNetCore.Identity;
using ToDoList.API.Token;
using ToDoList.API.ViewModels;
using ToDoList.API.ViewModels.AssignmentListVM;
using ToDoList.API.ViewModels.AssignmentVM;
using ToDoList.API.ViewModels.UserVM;
using ToDoList.Application.Configuration;
using ToDoList.Application.DTO;
using ToDoList.Application.Services;
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

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "ToDoList",
        Version = "v1",
        //Description = ""
    });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = @"Autenticação em JWT. \r\n\r\n
                        Ex: Bearer {token}",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Scheme = "Bearer",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

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

        cfg.CreateMap<LoginDTO, LoginViewModel>().ReverseMap();
    });
    builder.Services.AddSingleton(autoMapperConfig.CreateMapper());
}

builder.Services.AddSingleton(d => builder.Configuration);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IPasswordHasher<User>, Argon2PasswordHasher<User>>();

builder.Services.AddDbContext<ToDoListContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

builder.Services.AddScoped<ITokenGenerator, TokenGenerator>();

var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeyConfig.Secret));

builder.Services.AddAuthentication(authOptions =>
{
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer("Bearer", options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        IssuerSigningKey = secretKey,
        ValidateAudience = false,
        ValidateIssuer = false
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();