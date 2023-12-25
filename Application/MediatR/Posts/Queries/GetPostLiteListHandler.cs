using Application.Abstractions.Posts;
using Application.DTOs.Posts;
using Application.DTOs.Votes;
using AutoMapper;
using MediatR;
using System.Collections.Generic;

namespace Application.MediatR.Posts.Queries;

public record GetPostLiteList() : IRequest<IEnumerable<PostLiteDTO>>;

public class GetPostLiteListHandler : IRequestHandler<GetPostLiteList, IEnumerable<PostLiteDTO>>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public GetPostLiteListHandler(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<PostLiteDTO>> Handle(GetPostLiteList request, CancellationToken cancellationToken)
    {
        var postList = await _repository.GetAll();
        List<PostLiteDTO> postLiteList = new List<PostLiteDTO>();

        foreach (var post in postList)
        {
            if (post.Options == null || post.Options.Count == 0)
            {
                continue;
            }

            var votes = post.Options.Select(opt => new VoteLiteDTO()
            {
                Option = opt.Title,
                Count = opt.Votes.Count
            });

            PostLiteDTO postLiteDTO = new PostLiteDTO()
            {
                AuthorId = post.AuthorId,
                Title = post.Title,
                Votes = votes
            };

            postLiteList.Add(postLiteDTO);
        }

        return postLiteList;
    }
}
