using Application.Abstractions.Assignments;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;

namespace Application.MediatR.Assignments.QueriesHandlers;

//public record GetAllAssignments() : IRequest<IEnumerable<AssignmentDTO>>;

//public class GetAllAssignmentsHandler : IRequestHandler<GetAllAssignments, IEnumerable<AssignmentDTO>>
//{
//    private readonly IAssignmentRepository _assignmentRepository;
//    private readonly IMapper _mapper;

//    public GetAllAssignmentsHandler(IAssignmentRepository assignmentRepository, IMapper mapper)
//    {
//        _assignmentRepository = assignmentRepository;
//        _mapper = mapper;
//    }

//    public async Task<IEnumerable<AssignmentDTO>> Handle(GetAllAssignments request, CancellationToken cancellationToken)
//    {

//        var assignments = await _assignmentRepository.GetAll();
//        IEnumerable<AssignmentDTO> result = _mapper.Map<IEnumerable<AssignmentDTO>>(assignments);

//        return result;
//    }
//}
