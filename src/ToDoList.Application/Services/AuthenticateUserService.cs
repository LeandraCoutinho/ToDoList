using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ToDoList.Application.Configuration;
using ToDoList.Application.DTO;
using ToDoList.Application.Interfaces;
using ToDoList.Core.Exceptions;
using ToDoList.Domain.Contracts;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services;

public class AuthenticateUserService : IAuthenticateUser
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthenticateUserService(IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    private static string GenerateToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(KeyConfig.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    
    public async Task<AuthenticateLoginDTO> Authenticate(LoginDTO loginDto)
    {
        var loginExist = await _userRepository.GetByEmail(loginDto.Email);
        if (loginExist == null)
            throw new DomainException("Não foi encontrdo cadastrado com esse email, por favor, insira um correto!");
        
        var result = _passwordHasher.VerifyHashedPassword(loginExist, loginExist.Password, loginDto.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new DomainException("Email e/ou senha estão incorretos, por favor, corríja-os!");

        return new AuthenticateLoginDTO
        {
            Id = loginExist.Id,
            Email = loginExist.Email,
            Name = loginExist.Name,
            Token = GenerateToken(loginExist)
        };
    }
}