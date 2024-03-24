using Application.Abstractions.Complains;
using Application.Abstractions.Files;
using Application.DTOs.Complains;
using Application.DTOs.General;
using Application.DTOs.Posts;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.MediatR.Complains.Queries;

public record GetComplainList(PaginationRequestDTO PaginationRequestDTO) : IRequest<PaginationResponseDTO<GetComplainDTO>>;

public class GetComplainListHandler : IRequestHandler<GetComplainList, PaginationResponseDTO<GetComplainDTO>>
{
    private readonly IComplainRepository _complainRepository;
    private readonly IMediaLinkGeneratorService _mediaLinkGeneratorService;

    public GetComplainListHandler(IComplainRepository repository,
        IMediaLinkGeneratorService mediaLinkGeneratorService)
    {
        _complainRepository = repository;
        _mediaLinkGeneratorService = mediaLinkGeneratorService;
    }

    public async Task<PaginationResponseDTO<GetComplainDTO>> Handle(GetComplainList request, CancellationToken cancellationToken)
    {
        string? searchQuery = request.PaginationRequestDTO.SearchQuery;
        string? sortColumn = request.PaginationRequestDTO.SortColumn;
        string? sortOrder = request.PaginationRequestDTO.SortOrder;
        int page = request.PaginationRequestDTO.Page;
        int pageSize = request.PaginationRequestDTO.PageSize;

        var complainList = await _complainRepository.GetComplainList();

        if (!string.IsNullOrEmpty(searchQuery))
        {
            complainList = complainList.Where(complain => complain.Post.Title.ToLower().Contains(searchQuery.ToLower())
            || complain.User.Name.ToLower().Contains(searchQuery.ToLower())).ToList();
        }

        Expression<Func<GetComplainDTO, object>> keySortSelector = sortColumn?.ToLower() switch
        {
            "title" => complain => complain.Post.Title,
            "name" => complain => complain.User.Name,
            _ => complain => complain.Id
        };

        if (sortOrder?.ToLower() == "desc")
            complainList = complainList.AsQueryable().OrderByDescending(keySortSelector).ToList();
        else
        {
            complainList = complainList.AsQueryable().OrderBy(keySortSelector).ToList();
        }

        foreach (var complain in complainList)
        {
            if (complain.User.AvatarId is not null)
            {
                complain.User.AvatarLink = _mediaLinkGeneratorService.GenerateMediaLink(complain.User.AvatarId);
            }
        }

        complainList = complainList.OrderByDescending(x => x.Created);

        return new PaginationResponseDTO<GetComplainDTO>()
        {
            ResponseList = complainList.Skip((page - 1) * pageSize).Take(pageSize),
            TotalCount = complainList.Count()
        };
    }
}
