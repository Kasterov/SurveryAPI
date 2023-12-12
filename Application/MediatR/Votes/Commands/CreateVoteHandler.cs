using Application.Abstractions.Votes;
using Application.DTOs.Posts;
using Application.DTOs.Votes;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.Votes.Commands;

public record CreateVote(CreateVoteDTO dto) : IRequest<VoteDTO>;

public class CreateVoteHandler : IRequestHandler<CreateVote, VoteDTO>
{
    private readonly IVoteRepository _repository;
    private readonly IMapper _mapper;

    public CreateVoteHandler(
        IVoteRepository repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<VoteDTO> Handle(CreateVote request, CancellationToken cancellationToken)
    {
        CreateVoteDTO dto = request.dto;

        var post = new Vote()
        {
            PoolOptionId = dto.PoolOptionId,
            UserId = dto.UserId
        };

        var entity = await _repository.Add(post);
        var result = _mapper.Map<VoteDTO>(entity);

        return result;
    }
}