using System.Data;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using ToDoList.Application.DTO;
using ToDoList.Application.Interfaces;
using ToDoList.Core.Exceptions;
using ToDoList.Domain.Contracts;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services;

public class AssignmentListService : IAssignmentListService
{
    private readonly IAssignmentListRepository _assignmentListRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AssignmentListService(IAssignmentListRepository assignmentListRepository, IMapper mapper, IHttpContextAccessor contextAccessor)
    {
        _assignmentListRepository = assignmentListRepository;
        _mapper = mapper;
        _httpContextAccessor = contextAccessor;
    }

    public async Task<AssignmentListDTO> Create(AssignmentListDTO assignmentListDto)
    {
        var assignmetListExist = await _assignmentListRepository.GetByNameAndId(assignmentListDto.Name, GetUserId());
        if (assignmetListExist != null)
            throw new DomainException("Já existe uma lista de tarefa cadastrada com esse nome!");
        
        var assignmentList = _mapper.Map<AssignmentList>(assignmentListDto);
        assignmentList.Validate();

        assignmentList.UserId = GetUserId();
        
        var assignmentListCreated = await _assignmentListRepository.Create(assignmentList);
        return _mapper.Map<AssignmentListDTO>(assignmentListCreated);
    }

    public async Task<AssignmentListDTO> Update(AssignmentListDTO assignmentListDto)
    {
        var assignmentListExist = await _assignmentListRepository.Get(assignmentListDto.Id);
        if (assignmentListExist == null)
            throw new DataException("Não existe nenhuma lista de tarefa cadastrada com esse Id");
        
        var assignmentList = _mapper.Map<AssignmentList>(assignmentListDto);
        assignmentList.Validate();

        assignmentList.UserId = GetUserId();

        var assignmentListUpdated = await _assignmentListRepository.Update(assignmentList);
        return _mapper.Map<AssignmentListDTO>(assignmentListUpdated); 
    }

    public async Task Remove(int id)
    {
        await _assignmentListRepository.Remove(id);
    }

    public async Task<AssignmentListDTO> Get(int id)
    {
        var assignmentList = await _assignmentListRepository.Get(id);

        return _mapper.Map<AssignmentListDTO>(assignmentList);
    }

    public async Task<List<AssignmentListDTO>> GetAll()
    {
        var allAssignmetLists = await _assignmentListRepository.Get();

        return _mapper.Map<List<AssignmentListDTO>>(allAssignmetLists);
    }

    public async Task<AssignmentListDTO> GetByName(string name)
    {
        var assignmentList = await _assignmentListRepository.GetByName(name);

        return _mapper.Map<AssignmentListDTO>(assignmentList);
    }

    public async Task<List<AssignmentListDTO>> SearchByName(string name)
    {
        var assignmentList = await _assignmentListRepository.SearchByName(name);

        return _mapper.Map<List<AssignmentListDTO>>(assignmentList);
    }
    
    
    private int GetUserId()
    {
        var claim = _httpContextAccessor?.HttpContext?.User.Claims.FirstOrDefault(
            x => x.Type == "Id");
        if (claim == null)
            throw new DomainException("Usuário inválido!");
        return string.IsNullOrWhiteSpace(claim.Value) ? 0 : int.Parse(claim.Value);
    }
    
    
}