using Application.Abstractions.Posts;
using Application.Abstractions.Users;
using Application.DTOs.Posts;
using Application.DTOs.Users;
using Application.MediatR.Users.Commands;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.Posts.Commands;

public record CreatePost(CreatePostDTO dto) : IRequest<PostDTO>;

public class CreatePostHandler : IRequestHandler<CreatePost, PostDTO>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly IIdentity _identity;

    public CreatePostHandler(
        IPostRepository repository,
        IMapper mapper,
        IIdentity identity)
    {
        _repository = repository;
        _mapper = mapper;
        _identity = identity;
    }

    public async Task<PostDTO> Handle(CreatePost request, CancellationToken cancellationToken)
    {
        CreatePostDTO dto = request.dto;

        var post = new Post()
        {
            Title = dto.Title,
            Description = dto.Description,
            AuthorId = Convert.ToInt32(_identity.UserId),
            IsMultiple = dto.IsMultiple,
            Options = dto.Options.Select(opt => new PoolOption()
            {
                Title = opt.Title
            }).ToList(),
        };

        var entity = await _repository.Add(post);
        var result = _mapper.Map<PostDTO>(entity);

        return result;
    }
}
