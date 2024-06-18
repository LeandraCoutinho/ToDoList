using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Token;
using ToDoList.API.Utilities;
using ToDoList.API.ViewModels;
using ToDoList.Application.DTO;
using ToDoList.Application.Interfaces;
using ToDoList.Core.Exceptions;

namespace ToDoList.API.Controllers;

[ApiController]

public class AuthController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IAuthenticateUser _authenticateUser;

    public AuthController(IMapper mapper, IAuthenticateUser authenticateUser)
    {
        _mapper = mapper;
        _authenticateUser = authenticateUser;
    }

    [HttpPost]
    [Route("api/v1/auth/login")]
    public async Task<IActionResult> Login([FromBody] LoginViewModel loginViewModel)
    {
        try
        {
            var userExist = _mapper.Map<LoginDTO>(loginViewModel);

            var token = await _authenticateUser.Authenticate(userExist);
            return Ok(new ResultViewModel
            {
                Message = "Token gerado com sucesso!",
                Success = true,
                Data = token
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(401, Responses.ApplicationErrorMessage());
        }
    }
}