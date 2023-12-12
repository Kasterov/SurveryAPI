using Application.Abstractions.Assignments;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Assignments.QueriesHandlers;

//public record GetAssignmentById(int Id) : IRequest<AssignmentDTO>;

//public class GetAllAssignmentByIdHandler : IRequestHandler<GetAssignmentById, AssignmentDTO>
//{
//    private readonly IAssignmentRepository _assignmentRepository;
//    private readonly IMapper _mapper;

//    public GetAllAssignmentByIdHandler(IAssignmentRepository assignmentRepository, IMapper mapper)
//    {
//        _assignmentRepository = assignmentRepository;
//        _mapper = mapper;
//    }

//    public async Task<AssignmentDTO> Handle(GetAssignmentById request, CancellationToken cancellationToken)
//    {

//        var assignment = await _assignmentRepository.GetById(request.Id);
//        AssignmentDTO result = _mapper.Map<AssignmentDTO>(assignment);

//        return result;
//    }
//}