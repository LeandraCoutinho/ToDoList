using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoList.API.Utilities;
using ToDoList.API.ViewModels;
using ToDoList.API.ViewModels.AssignmentVM;
using ToDoList.Application.DTO;
using ToDoList.Application.Interfaces;
using ToDoList.Core.Exceptions;

namespace ToDoList.API.Controllers;

[Route(("api/[controller]"))]
[ApiController]

public class AssignmentController : ControllerBase
{
    private readonly IAssignmentService _assignmentService;
    private readonly IMapper _mapper;

    public AssignmentController(IAssignmentService assignmentService, IMapper mapper)
    {
        _assignmentService = assignmentService;
        _mapper = mapper;
    }
    
    [HttpPost]
    [Route("/api/v1/assignment/create")]
    public async Task<IActionResult> Create([FromBody] CreateAssignmentViewModel createAssignmentViewModel)
    {
        try
        {
            var assigmentDto = _mapper.Map<AssignmentDTO>(createAssignmentViewModel);
            var assignmentCreated = await _assignmentService.Create(assigmentDto);
            
            return Ok(new ResultViewModel
            {
                Message = "Tarefa criada com sucesso!",
                Success = true,
                Data = assignmentCreated
            });
        }
        catch (DomainException ex)
        {
            return BadRequest(Responses.DomainErrorMessage(ex.Message, ex.Errors));
        }
        catch (Exception ex)
        {
            return StatusCode(500, Responses.ApplicationErrorMessage());
        }
    }

    [HttpPut]
    [Route("api/v1/assignment/update")]
    public async Task<IActionResult> Update([FromBody] UpdateAssignmentViewModel updateAssignmentViewModel)
    {
        try
        {
            var assignmentDto = _mapper.Map<AssignmentDTO>(updateAssignmentViewModel);
            var assignmentUpdated = await _assignmentService.Update(assignmentDto);
            return Ok(new ResultViewModel
            {
                Message = "Tarefa atualizada com sucesso!",
                Success = true,
                Data = assignmentUpdated
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

    [HttpDelete]
    [Route("/api/v1/assignment/remove{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            var assignment = await _assignmentService.Get(id);

            if (assignment == null)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhuma tarefa foi encontrada com o Id informado.",
                    Success = true,
                    Data = assignment
                });

            await _assignmentService.Remove(id);
            return Ok(new ResultViewModel
            {
                Message = "Tarefa removida com sucesso!",
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

    [HttpGet]
    [Route("/api/v1/assignment/get/{id}")]
    public async Task<IActionResult> Get(int id)
    {
        try
        {
            var assignment = await _assignmentService.Get(id);

            if (assignment == null)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhuma tarefa foi encontrada com o Id informado!",
                    Success = true,
                    Data = assignment
                });

            return Ok(new ResultViewModel
            {
                Message = "Tarefa encontra com sucesso!",
                Success = true,
                Data = assignment
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

    [HttpGet]
    [Route("/api/v1/assignment/get-all")]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var allAssignments = await _assignmentService.GelAll();

            if (allAssignments.Count == 0)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhuma tarefa cadastrada!",
                    Success = true,
                    Data = allAssignments
                });

            return Ok(new ResultViewModel
            {
                Message = "Tarefas encontradas com sucesso!",
                Success = true,
                Data = allAssignments
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

    [HttpGet]
    [Route("/api/v1/assignment/get-concluded")]
    public async Task<IActionResult> GetConcluded()
    {
        try
        {
            var assignmentsConcluded = await _assignmentService.GetConcluded();
            
            if (assignmentsConcluded.Count == 0)
                return Ok(new ResultViewModel
                {
                    Message = "Nenhuma tarefa concluída!",
                    Success = true,
                    Data = assignmentsConcluded
                });
            
            return Ok(new ResultViewModel
            {
                Message = "Tarefas concluídas encontradas com sucesso!",
                Success = true,
                Data = assignmentsConcluded
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