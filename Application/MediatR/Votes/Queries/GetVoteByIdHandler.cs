using Application.Abstractions.Users;
using Application.Abstractions.Votes;
using Application.DTOs.Users;
using Application.DTOs.Votes;
using Application.MediatR.Users.Queries;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Votes.Queries;

public record GetVoteById(int Id) : IRequest<VoteDTO>;

public class GetVoteByIdHandler : IRequestHandler<GetVoteById, VoteDTO>
{
    private readonly IVoteRepository _repository;
    private readonly IMapper _mapper;

    public GetVoteByIdHandler(IVoteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<VoteDTO> Handle(GetVoteById request, CancellationToken cancellationToken)
    {
        var vote = await _repository.GetById(request.Id);
        var result = _mapper.Map<VoteDTO>(vote);

        return result;
    }
}
