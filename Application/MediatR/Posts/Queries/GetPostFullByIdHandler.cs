using Application.Abstractions.Files;
using Application.Abstractions.Posts;
using Application.DTOs.Posts;
using Application.DTOs.Votes;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.Posts.Queries;

public record GetPostFull(int Id) : IRequest<PostFullDTO?>;

public class GetPostFullByIdHandler : IRequestHandler<GetPostFull, PostFullDTO?>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly IMediaLinkGeneratorService _mediaLinkGeneratorService;

    public GetPostFullByIdHandler(IPostRepository repository,
        IMapper mapper,
        IMediaLinkGeneratorService mediaLinkGeneratorService)
    {
        _repository = repository;
        _mapper = mapper;
        _mediaLinkGeneratorService = mediaLinkGeneratorService;
    }

    public async Task<PostFullDTO?> Handle(GetPostFull request, CancellationToken cancellationToken)
    {
        var postFullDTO = await _repository.GetPostFullDTO(request.Id);

        if (postFullDTO == null) 
        {
            return null; 
        }

        if ( postFullDTO.Author.AvatarId is not null)
        {
            postFullDTO.Author.AvatarLink = _mediaLinkGeneratorService.GenerateMediaLink(postFullDTO.Author.AvatarId);
        }

        postFullDTO.TotalCount = postFullDTO.Votes.Sum(vote => vote.Count);

        return postFullDTO;
    }
}
