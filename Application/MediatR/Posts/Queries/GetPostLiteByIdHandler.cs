using Application.Abstractions.Posts;
using Application.DTOs.Posts;
using Application.DTOs.Votes;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.Posts.Queries;

public record GetPostLiteById(int Id) : IRequest<PostLiteDTO>;

public class GetPostLiteByIdHandler : IRequestHandler<GetPostLiteById, PostLiteDTO>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;

    public GetPostLiteByIdHandler(IPostRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<PostLiteDTO> Handle(GetPostLiteById request, CancellationToken cancellationToken)
    {
        var post = await _repository.GetById(request.Id);

        var votes = post.Options.Select(opt => new VoteLiteDTO()
        {
            Option = opt.Title,
            Count = opt.Votes.Count
        });

        PostLiteDTO postLiteDTO = new PostLiteDTO()
        {
            //Author = post.Author,
            Title = post.Title,
            Votes = votes
        };

        return postLiteDTO;
    }
}
