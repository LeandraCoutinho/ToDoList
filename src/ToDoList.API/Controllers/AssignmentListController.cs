using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Utilities;
using ToDoList.API.ViewModels;
using ToDoList.API.ViewModels.AssignmentListVM;
using ToDoList.Application.DTO;
using ToDoList.Application.Interfaces;
using ToDoList.Core.Exceptions;
using ToDoList.Domain.Entities;

namespace ToDoList.API.Controllers;

[Route(("api/[controller]"))]
[ApiController]

public class AssignmentListController : ControllerBase
{
    private readonly IAssignmentListService _assignmentListService;
    private readonly IMapper _mapper;

    public AssignmentListController(IAssignmentListService assignmentListService, IMapper mapper)
    {
        _assignmentListService = assignmentListService;
        _mapper = mapper;
    }

    [Authorize]
    [Route("/api/v1/assignmentList/create")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateAssignmentListViewModel createAssignmentListViewModel)
    {
        try
        {
            var assignmentListDto = _mapper.Map<AssignmentListDTO>(createAssignmentListViewModel);
            var assignmentListCreated = await _assignmentListService.Create(assignmentListDto);
            return Ok(new ResultViewModel
            {
                Message = "Lista de tarefa criada com sucesso",
                Success = true,
                Data = assignmentListCreated
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
    [Route("api/v1/assignmentList/update")]
    public async Task<IActionResult> Update(UpdateAssignmentListViewModel updateAssignmentListViewModel)
    {
        try
        {
            var assignmentListDto = _mapper.Map<AssignmentListDTO>(updateAssignmentListViewModel);
            var assignmentListUpdated = await _assignmentListService.Update(assignmentListDto);
            return Ok(new ResultViewModel
            {
                Message = "Lista de tarefa atualizada com sucesso!",
                Success = true,
                Data = assignmentListUpdated
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
    [Route("/api/v1/assignmentList/remove{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            var assignmnetList = await _assignmentListService.Get(id);
            
            if (assignmnetList == null)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhuma tarefa foi encontrada com o Id informado.",
                    Success = true,
                    Data = assignmnetList
                });
            
            await _assignmentListService.Remove(id);
            return Ok(new ResultViewModel
            {
                Message = "Lista de tarefa removida com sucesso!",
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
    [Route("/api/v1/assignmentList/get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var assignmentList = await _assignmentListService.Get(id);

            if (assignmentList == null)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhuma lista de tarefa foi encontrada com o Id informado.",
                    Success = true,
                    Data = assignmentList
                });

            return Ok(new ResultViewModel
            {
                Message = "Lista de tarefa encontrada com sucesso!",
                Success = true,
                Data = assignmentList
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
    [Route("/api/v1/assignmentList/get-all")]
    public async Task<IActionResult> GetAll() // aqui é uma lista
    {
        try
        {
            var assignmentLists = await _assignmentListService.GetAll();

            if (assignmentLists.Count == 0)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhuma lista cadastrada!",
                    Success = true,
                    Data = assignmentLists
                });

            return Ok(new ResultViewModel
            {
                Message = "Lista de tarefas encontradas com sucesso!",
                Success = true,
                Data = assignmentLists
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
    [Route("/api/v1/users/get-by-name")]
    public async Task<IActionResult> GetByName([FromQuery] string name)
    {
        try
        {
            var assignmentList = await _assignmentListService.GetByName(name);

            if (assignmentList == null)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhuma lista de tarefa foi encontrada com esse nome.",
                    Success = true,
                    Data = assignmentList
                });

            return Ok(new ResultViewModel
            {
                Message = "Lista de tarefa encontrada com sucesso!",
                Success = true,
                Data = assignmentList
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
    [Route("/api/v1/assignment/search-by-name")]
    public async Task<IActionResult> SearchByName([FromQuery]string name)
    {
        try
        {
            var assignemntList = await _assignmentListService.SearchByName(name);

            if (assignemntList.Count == 0)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhuma lista de tarefa foi encontra com o nome informado.",
                    Success = true,
                    Data = null
                });

            return Ok(new ResultViewModel
            {
                Message = "Lista de tarefa encontrada com sucesso!",
                Success = true,
                Data = assignemntList
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