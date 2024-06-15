using System.Data;
using AutoMapper;
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

    public AssignmentListService(IAssignmentListRepository assignmentListRepository, IMapper mapper)
    {
        _assignmentListRepository = assignmentListRepository;
        _mapper = mapper;
    }

    public async Task<AssignmentListDTO> Create(AssignmentListDTO assignmentListDto)
    {
        var assignmetListExist = await _assignmentListRepository.GetByName(assignmentListDto.Name);

        if (assignmetListExist != null)
            throw new DomainException("Já existe uma lista de tarefa cadastrada com esse nome!");

        var assignmentList = _mapper.Map<AssignmentList>(assignmentListDto);
        assignmentList.Validate();

        var assignmentListCreated = await _assignmentListRepository.Create(assignmentList);

        return _mapper.Map<AssignmentListDTO>(assignmentListCreated);
    }

    public async Task<AssignmentListDTO> Update(AssignmentListDTO assignmentListDto)
    {
        var assignmentListExist = await _assignmentListRepository.Get(assignmentListDto.Id);

        if (assignmentListExist == null)
            throw new DataException("Não existe nenhuma lista de tarefa cadastrada com esse Id");

        var assignmentList = _mapper.Map<AssignmentList>(assignmentListDto);

        var assignmentListUpdated = await _assignmentListRepository.Update(assignmentList);

        return _mapper.Map<AssignmentListDTO>(assignmentListUpdated); }

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

    public async Task<List<AssignmentListDTO>> GetByName(string name)
    {
        var assignmentList = await _assignmentListRepository.GetByName(name);

        return _mapper.Map<List<AssignmentListDTO>>(assignmentList);
    }

    public async Task<List<AssignmentListDTO>> SearchByName(string name)
    {
        var assignmentList = await _assignmentListRepository.SearchByName(name);

        return _mapper.Map<List<AssignmentListDTO>>(assignmentList);
    }
}