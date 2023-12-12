using Domain.Entities;
using MediatR;
using System;
using Application.Abstractions.Assignments;
using AutoMapper;
using Domain.Events;

namespace Application.MediatR.Assignments.CommandsHandlers;

//public record CreateAssignment(CreateAssignmentDTO createDTO) : IRequest<AssignmentDTO>;

//public class CreateAssignmentHandler : IRequestHandler<CreateAssignment, AssignmentDTO>
//{
//    private readonly IAssignmentRepository _assignmentRepository;
//    private readonly IMapper _mapper;

//    public CreateAssignmentHandler(
//        IAssignmentRepository assignmentRepository,
//        IMapper mapper)
//    {
//        _assignmentRepository = assignmentRepository;
//        _mapper = mapper;
//    }

//    public async Task<AssignmentDTO> Handle(CreateAssignment request, CancellationToken cancellationToken)
//    {
//        CreateAssignmentDTO createAssignmentDTO = request.createDTO;

//        var assignment = new Assignment
//        {
//            Name = createAssignmentDTO.Name,
//            Description = createAssignmentDTO.Description,
//            Type = createAssignmentDTO.Type,
//            ToDate = createAssignmentDTO.ToDate,
//            TicketId = createAssignmentDTO.TicketId,
//        };

//        assignment.AddDomainEvent(new AssignmentCreateEvent(assignment));

//        var addedAssignment = await _assignmentRepository.AddAssignment(assignment);
//        AssignmentDTO result = _mapper.Map<AssignmentDTO>(addedAssignment);

//        return result;
//    }
//}
