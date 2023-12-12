using Application.Abstractions.Posts;
using Application.Abstractions.Votes;
using Application.DTOs.Posts;
using Application.DTOs.Votes;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.Votes.Commands;

public record DeleteVoteById(int Id) : IRequest<VoteDTO>;

public class DeleteVoteByIdHandler : IRequestHandler<DeleteVoteById, VoteDTO>
{
    private readonly IVoteRepository _repository;
    private readonly IMapper _mapper;

    public DeleteVoteByIdHandler(IVoteRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<VoteDTO> Handle(DeleteVoteById request, CancellationToken cancellationToken)
    {
        var vote = await _repository.Delete(request.Id);
        var result = _mapper.Map<VoteDTO>(vote);

        return result;
    }
}
