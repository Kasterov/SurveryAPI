using Application.Abstractions.Assignments;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.Assignments.CommandsHandlers;

//public record EditAssignment(EditAssignmentDTO editDto) : IRequest<AssignmentDTO>;
//public class EditAssignmentHandler : IRequestHandler<EditAssignment, AssignmentDTO>
//{
//    private readonly IAssignmentRepository _assignmentRepository;
//    private readonly IMapper _mapper;

//    public EditAssignmentHandler(IAssignmentRepository assignmentRepository, IMapper mapper)
//    {
//        _assignmentRepository = assignmentRepository;
//        _mapper = mapper;
//    }

//    public async Task<AssignmentDTO> Handle(EditAssignment request, CancellationToken cancellationToken)
//    {
//        EditAssignmentDTO createAssignmentDTO = request.editDto;

//        var assignment = new Assignment
//        {
//            Id = createAssignmentDTO.Id,
//            Name = createAssignmentDTO.Name,
//            Description = createAssignmentDTO.Description,
//            Type = createAssignmentDTO.Type,
//            ToDate = createAssignmentDTO.ToDate,
//        };

//        var addedAssignment = await _assignmentRepository.UpdateAssignment(assignment);
//        AssignmentDTO result = _mapper.Map<AssignmentDTO>(addedAssignment);

//        return result;
//    }
//}