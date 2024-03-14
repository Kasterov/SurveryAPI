using Application.Abstractions.Files;
using Application.Abstractions.Posts;
using Application.DTOs.Common;
using Application.DTOs.General;
using Application.DTOs.Posts;
using Application.DTOs.Users;
using Application.DTOs.Votes;
using AutoMapper;
using MediatR;
using System.Linq.Expressions;

namespace Application.MediatR.Posts.Queries;

public record GetPostLiteList(PaginationRequestDTO PaginationRequestDTO) : IRequest<PaginationResponseDTO<PostLiteDTO>>;

public class GetPostLiteListHandler : IRequestHandler<GetPostLiteList, PaginationResponseDTO<PostLiteDTO>>
{
    private readonly IPostRepository _repository;
    private readonly IMediaLinkGeneratorService _mediaLinkGeneratorService;
    private readonly IMapper _mapper;

    public GetPostLiteListHandler(
        IPostRepository repository,
        IMapper mapper,
        IMediaLinkGeneratorService mediaLinkGeneratorService)
    {
        _repository = repository;
        _mapper = mapper;
        _mediaLinkGeneratorService = mediaLinkGeneratorService;
    }

    public async Task<PaginationResponseDTO<PostLiteDTO>> Handle(GetPostLiteList request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.PaginationRequestDTO.SearchQuery;
        string?  sortColumn = request.PaginationRequestDTO.SortColumn;
        string? sortOrder = request.PaginationRequestDTO.SortOrder;
        int page = request.PaginationRequestDTO.Page;
        int pageSize = request.PaginationRequestDTO.PageSize;

        var postList = await _repository.GetAll();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            postList = postList.Where(post => post.Title.ToLower().Contains(searchQuery.ToLower()) 
            || post.Author.Name.ToLower().Contains(searchQuery.ToLower())).ToList();
        }

        Expression<Func<PostLiteDTO, object>> keySortSelector = sortColumn?.ToLower() switch
        {
            "name" => product => product.Title,
            _ => product => product.Id
        };

        if (sortOrder?.ToLower() == "desc")
            postList = postList.AsQueryable().OrderByDescending(keySortSelector).ToList();
        else
        {
            postList = postList.AsQueryable().OrderBy(keySortSelector).ToList();
        }

        foreach (var post in postList)
        {
            if (post.Author.AvatarId is not null)
            {
                post.Author.AvatarLink = _mediaLinkGeneratorService.GenerateMediaLink(post.Author.AvatarId);
            }

            post.TotalCount = post.Votes.Sum(vote => vote.Count);
        }

        postList = postList.OrderByDescending(x => x.Created);

        return new PaginationResponseDTO<PostLiteDTO>()
        {
            ResponseList = postList.Skip((page - 1) * pageSize).Take(pageSize),
            TotalCount = postList.Count()
        };
    }
}
