using Application.Abstractions.Posts;
using Application.Abstractions.Users;
using Application.DTOs.Posts;
using AutoMapper;
using Domain.Entities;
using MediatR;
using IIdentity = Application.Abstractions.Users.IIdentity;

namespace Application.MediatR.Posts.Commands;

public record EditPost(PostEditDTO dto) : IRequest<bool>;

public class EditPostHandler : IRequestHandler<EditPost, bool>
{
    private readonly IPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly IIdentity _identity;

    public EditPostHandler(
        IPostRepository repository,
        IMapper mapper,
        IIdentity identity)
    {
        _repository = repository;
        _mapper = mapper;
        _identity = identity;
    }

    public async Task<bool> Handle(EditPost request, CancellationToken cancellationToken)
    {
        var postUpdate = _mapper.Map<Post>(request.dto);

        postUpdate.AuthorId = Convert.ToInt32(_identity.UserId);

        var postUpdated = await _repository.Update(postUpdate);

        if (postUpdated is null)
        {
            return false;
        }

        return true;
    }
}
