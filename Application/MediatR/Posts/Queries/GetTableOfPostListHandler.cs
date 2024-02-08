using Application.Abstractions.Posts;
using Application.Abstractions.Users;
using Application.DTOs.General;
using Application.DTOs.Posts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace Application.MediatR.Posts.Queries;

public record GetTableOfPostList(PaginationRequestDTO PaginationRequestDTO) : IRequest<PaginationResponseDTO<PostTableDTO>>;

public class GetTableOfPostListHandler : IRequestHandler<GetTableOfPostList, PaginationResponseDTO<PostTableDTO>>
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

    public async Task<PaginationResponseDTO<PostTableDTO>> Handle(GetTableOfPostList request, CancellationToken cancellationToken)
    {
        string searchQuery = request.PaginationRequestDTO.SearchQuery;
        string sortColumn = request.PaginationRequestDTO.SortColumn;
        string sortOrder = request.PaginationRequestDTO.SortOrder;
        int page = request.PaginationRequestDTO.Page;
        int pageSize = request.PaginationRequestDTO.PageSize;

        int currentUserId = Convert.ToInt32(_identity.UserId);

        IEnumerable<PostTableFullDTO> postList = new List<PostTableFullDTO>();

        postList = await _postRepository.GetTablePostList(currentUserId);
        
        var postTableDTOList = new List<PostTableDTO>();

        foreach (var post in postList)
        {
            var allVotesForPost = post.PoolOptions
                .SelectMany(po => po.Votes);

            int voteCount = allVotesForPost.Count();

            int peopleCount = allVotesForPost
                .Select(vote => vote.User.Id)
                .Distinct()
                .Count();

            var postTableDTO = new PostTableDTO()
            {
                Id = post.Id,
                Title = post.Title,
                Status = post.Status,
                Votes = voteCount,
                People = peopleCount,
                Created = post.Created,
                LastModified = post.LastModified
            };

            postTableDTOList.Add(postTableDTO);
        }

        if (!string.IsNullOrEmpty(searchQuery))
        {
            postTableDTOList = postTableDTOList.Where(post => post.Title.Contains(searchQuery)).ToList();
        }

        Expression<Func<PostTableDTO, object>> keySortSelector = sortColumn?.ToLower() switch
        {
            "title" => product => product.Title,
            "votes" => product => product.Votes,
            "people" => product => product.People,
            _ => product => product.Id
        };

        if (sortOrder?.ToLower() == "desc")
            postTableDTOList = postTableDTOList.AsQueryable().OrderByDescending(keySortSelector).ToList();
        else
        {
            postTableDTOList = postTableDTOList.AsQueryable().OrderBy(keySortSelector).ToList();
        }

        return new PaginationResponseDTO<PostTableDTO>()
        {
            ResponseList = postTableDTOList.Skip((page - 1) * pageSize).Take(pageSize),
            TotalCount = postTableDTOList.Count()
        };
    }
}
