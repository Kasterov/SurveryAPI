using Application.Abstractions.Posts;
using Application.Abstractions.Users;
using Application.DTOs.Posts;
using AutoMapper;
using MediatR;


namespace Application.MediatR.Posts.Queries;

public record GetTableOfPostList() : IRequest<IEnumerable<PostTableDTO>>;

public class GetTableOfPostListHandler : IRequestHandler<GetTableOfPostList, IEnumerable<PostTableDTO>>
{
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;
    private readonly IIdentity _identity;

    public GetTableOfPostListHandler(
        IPostRepository postRepository,
        IMapper mapper,
        IIdentity identity)
    {
        _postRepository = postRepository;
        _mapper = mapper;
        _identity = identity;
    }

    public async Task<IEnumerable<PostTableDTO>> Handle(GetTableOfPostList request, CancellationToken cancellationToken)
    {
        int currentUserId = Convert.ToInt32(_identity.UserId);

        var postList = await _postRepository.GetTablePostList(currentUserId);
        var postTableDTOList = new List<PostTableDTO>();

        foreach (var post in postList)
        {
            int voteCount = post.PoolOptions
                .SelectMany(po => po.Votes).Count();

            int peopleCount = post.PoolOptions
                .SelectMany(po => po.Votes
                .Select(vote => vote.User.Id))
                .Distinct()
                .Count();


            var postTableDTO = new PostTableDTO()
            {
                Id = post.Id,
                Title = post.Title,
                Status = post.Status,
                Votes = voteCount,
                People = peopleCount
            };

            postTableDTOList.Add(postTableDTO);
        }

        return postTableDTOList;
    }
}
