using Application.Abstractions.Posts;
using Application.Abstractions.Users;
using Application.Abstractions.Votes;
using Application.DTOs.PoolOptions;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Posts.Queries;

public record PoolOptionsForVoteByPostId(int Id) : IRequest<PoolOptionsForVoteDTO>;

public class GetPoolOptionsByPostIdHandler : IRequestHandler<PoolOptionsForVoteByPostId, PoolOptionsForVoteDTO>
{
    private readonly IPostRepository _postRepository;
    private readonly IVoteRepository _voteRepository;
    private readonly IMapper _mapper;
    private readonly IIdentity _identity;

    public GetPoolOptionsByPostIdHandler(IPostRepository postRepository,
        IVoteRepository voteRepository,
        IMapper mapper,
        IIdentity identity)
    {
        _postRepository = postRepository;
        _voteRepository = voteRepository;
        _mapper = mapper;
        _identity = identity;
    }


    public async Task<PoolOptionsForVoteDTO> Handle(PoolOptionsForVoteByPostId request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetById(request.Id);

        var poolOptionsForVoteDTO = _mapper.Map<PoolOptionsForVoteDTO>(post);

        poolOptionsForVoteDTO.IsRevotable = post.IsRevotable;

        if (_identity.UserId == null)
        {
            return poolOptionsForVoteDTO;
        }

        int userId = Convert.ToInt32(_identity.UserId);

        var votedIdList = post.Options
            .Where(p => p.Votes.Where(v => v.UserId == userId).Select(v => v.PoolOptionId).ToList().Contains(p.Id))
            .Select(p => p.Id)
            .ToList();

        foreach (var optionId in votedIdList)
        {
            foreach (var option in poolOptionsForVoteDTO.Options)
            {
                if (optionId == option.Id)
                {
                    option.IsSelected = true;
                }
            }
        }

        return poolOptionsForVoteDTO;
    }
}
