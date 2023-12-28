using Application.Abstractions.Posts;
using Application.DTOs.PoolOptions;
using Application.DTOs.Posts;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Posts.Queries;

public record PoolOptionsForVoteByPostId(int Id) : IRequest<PoolOptionsForVoteDTO>;

public class GetPoolOptionsByPostIdHandler : IRequestHandler<PoolOptionsForVoteByPostId, PoolOptionsForVoteDTO>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public GetPoolOptionsByPostIdHandler(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<PoolOptionsForVoteDTO> Handle(PoolOptionsForVoteByPostId request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetById(request.Id);

        var poolOptionsForVoteDTO = new PoolOptionsForVoteDTO
        {
            Id = post.Id,
            Title = post.Title,
            Description = post.Description,
            Options = _mapper.Map<IEnumerable<PoolOptionBaseDTO>>(post.Options)
        };

        return poolOptionsForVoteDTO;
    }
}
