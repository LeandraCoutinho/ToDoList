using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Utilities;
using ToDoList.API.ViewModels;
using ToDoList.API.ViewModels.UserVM;
using ToDoList.Application.DTO;
using ToDoList.Application.Interfaces;
using ToDoList.Core.Exceptions;

namespace ToDoList.API.Controllers;

[Route(("api/[controller]"))]
[ApiController]

public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UserController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [Route("/api/v1/users/create")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserViewModel userViewModel)
    {
        try
        {
            var userDto = _mapper.Map<UserDTO>(userViewModel);
            var userCreated = await _userService.Create(userDto);
            return Ok(new ResultViewModel
            {
                Message = "Usuário criado com sucesso!",
                Success = true,
                Data = userCreated
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [Authorize]
    [HttpPut]
    [Route("api/v1/users/update")]
    public async Task<IActionResult> Update([FromBody] UpdateUserViewModel userUserViewModel)
    {
        try
        {
            var userDto = _mapper.Map<UserDTO>(userUserViewModel);
            var userUpdate = await _userService.Update(userDto);
            return Ok(new ResultViewModel
            {
                Message = "Usuário atualizado com sucesso!",
                Success = true,
                Data = userUpdate
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [Authorize]
    [HttpDelete]
    [Route("/api/v1/users/remove{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            var user = await _userService.Get(id);
            
            if (user == null)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhum usuário foi encontrado com o Id informado!",
                    Success = true,
                    Data = user
                });

            await _userService.Remove(id);
            return Ok(new ResultViewModel
            {
                Message = "Usuário removido com sucesso!",
                Success = true,
                Data = null
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [Authorize]
    [HttpGet]
    [Route("/api/v1/users/get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var user = await _userService.Get(id);

            if (user == null)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhum usuário foi encontrado com o Id informado!",
                    Success = true,
                    Data = user
                });
            return Ok(new ResultViewModel
            {
                Message = "Usuário encontrado com sucesso!",
                Success = true,
                Data = user
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [Authorize]
    [HttpGet]
    [Route("/api/v1/users/get-all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var allUsers = await _userService.GetAll();
            
            if (allUsers.Count == 0)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhum usuário cadastrado!",
                    Success = true,
                    Data = allUsers
                });
            
            return Ok(new ResultViewModel
            {
                Message = "Usuários encontrados com sucesso!",
                Success = true,
                Data = allUsers
            });
        }
        catch(DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch(Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [Authorize]
    [HttpGet]
    [Route("/api/v1/users/get-by-email")]
    public async Task<IActionResult> GetByEmail([FromQuery] string email)
    {
        try
        {
            var user = await _userService.GetByEmail(email);

            if (user == null)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhum usuário foi encontrado com o email informado",
                    Success = true,
                    Data = user
                });

            return Ok(new ResultViewModel
            {
                Message = "Usuário encontrado com sucesso!",
                Success = true,
                Data = user
            });
        }
        catch(DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch(Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [Authorize]
    [HttpGet]
    [Route("/api/v1/users/search-by-email")]
    public async Task<IActionResult> SearchByEmail([FromQuery] string email)
    {
        try
        {
            var allUsers = await _userService.SearchByEmail(email);

            if (allUsers.Count == 0)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhum usuário foi encontrado com o email informado!",
                    Success = true,
                    Data = null
                });

            return Ok(new ResultViewModel
            {
                Message = "Usuário encontrado com sucesso!",
                Success = true,
                Data = allUsers
            });
        }
        catch(DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch(Exception)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }
}