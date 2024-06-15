using AutoMapper;
using ToDoList.Application.DTO;
using ToDoList.Application.Interfaces;
using ToDoList.Core.Exceptions;
using ToDoList.Domain.Contracts;
using ToDoList.Domain.Entities;

namespace ToDoList.Application.Services;

public class AssignmentService : IAssignmentService
{
    private readonly IAssignmentRepository _assignmentRepository;
    private readonly IMapper _mapper;

    public AssignmentService(IAssignmentRepository assignmentRepository, IMapper mapper)
    {
        _assignmentRepository = assignmentRepository;
        _mapper = mapper;
    }

    public async Task<AssignmentDTO> Create(AssignmentDTO assignmentDto)
    {
        var assignmentExist = await _assignmentRepository.GetById(assignmentDto.Id, assignmentDto.UserId);

        if (assignmentExist != null)
            throw new DomainException("Já existe uma tarefa cadastrada com esse nome");

        var assignment = _mapper.Map<Assignment>(assignmentDto);
        assignment.Validate();

        var assignmentCreated = await _assignmentRepository.Create(assignment);

        return _mapper.Map<AssignmentDTO>(assignmentCreated);
    }

    public async Task<AssignmentDTO> Update(AssignmentDTO assignmentDto)
    {
        var assignmetExist = await _assignmentRepository.GetById(assignmentDto.Id, assignmentDto.UserId);

        if (assignmetExist == null)
            throw new DomainException("Não foi encontrada nenhuma tarefa com o nome informado");

        var assignmentUpdated = _mapper.Map<AssignmentDTO>(assignmentDto);

        return _mapper.Map<AssignmentDTO>(assignmentUpdated);
    }

    public async Task Remove(int id)
    {
        await _assignmentRepository.Remove(id);
    }

    public async Task<AssignmentDTO> Get(int id)
    {
        var assignment = await _assignmentRepository.Get(id);

        return _mapper.Map<AssignmentDTO>(assignment);
    }

    public async Task<List<AssignmentDTO>> GelAll()
    {
        var allAssignment = await _assignmentRepository.Get();

        return _mapper.Map<List<AssignmentDTO>>(allAssignment);
    }

    public async Task<List<AssignmentDTO>> GetConcluded()
    {
        var allConclueded = await _assignmentRepository.GetConcluded();

        return _mapper.Map<List<AssignmentDTO>>(allConclueded);
    }
}