using Application.Abstractions.Assignments;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.Assignments.CommandsHandlers;

//public record DeleteAssignment(int Id) : IRequest<AssignmentDTO>;

//public class DeleteAssignmentHandler : IRequestHandler<DeleteAssignment, AssignmentDTO>
//{
//    private readonly IAssignmentRepository _assignmentRepository;
//    private readonly IMapper _mapper;

//    public DeleteAssignmentHandler(IAssignmentRepository assignmentRepository, IMapper mapper)
//    {
//        _assignmentRepository = assignmentRepository;
//        _mapper = mapper;
//    }

//    public async Task<AssignmentDTO> Handle(DeleteAssignment request, CancellationToken cancellationToken)
//    {
//        var addedAssignment = await _assignmentRepository.DeleteAssignment(request.Id);
//        AssignmentDTO result = _mapper.Map<AssignmentDTO>(addedAssignment);

//        return result;
//    }
//}
