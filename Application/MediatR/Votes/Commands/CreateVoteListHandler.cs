using Application.Abstractions.Votes;
using Application.DTOs.Votes;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.Votes.Commands;

public record CreateVoteList(CreateVoteListDTO dto) : IRequest<IEnumerable<VoteDTO>>;

public class CreateVoteListHandler : IRequestHandler<CreateVoteList, IEnumerable<VoteDTO>>
{
    private readonly IVoteRepository _repository;
    private readonly IMapper _mapper;

    public CreateVoteListHandler(
        IVoteRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<VoteDTO>> Handle(CreateVoteList request, CancellationToken cancellationToken)
    {
        var voteList = request.dto.VoteIdList.Select(vote => new Vote()
        {
            PoolOptionId = vote,
            UserId = request.dto.UserId
        });

        var entities = await _repository.AddList(voteList);

        var mappedEntities = _mapper.Map<IEnumerable<VoteDTO>>(entities);

        return mappedEntities;
    }
}
