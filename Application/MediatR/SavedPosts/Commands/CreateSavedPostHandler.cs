using Application.Abstractions.SavedPosts;
using Application.Abstractions.Users;
using Application.DTOs.SavedPosts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.SavedPosts.Commands;

public record CreateSavedPost(CreateSavedPostDTO dto) : IRequest<bool>;

public class CreateSavedPostHandler : IRequestHandler<CreateSavedPost, bool>
{
    private readonly ISavedPostRepository _repository;
    private readonly IMapper _mapper;
    private readonly Abstractions.Users.IIdentity _iIdentity;
    public CreateSavedPostHandler(
        ISavedPostRepository repository,
        IMapper mapper,
        Abstractions.Users.IIdentity identity)
    {
        _repository = repository;
        _iIdentity = identity;
        _mapper = mapper;
    }

    public async Task<bool> Handle(CreateSavedPost request, CancellationToken cancellationToken)
    {
        var savedPost = _mapper.Map<SavedPost>(request.dto);

        if (savedPost == null)
        {
            return false;
        }

        int userId = Convert.ToInt32(_iIdentity.UserId);
        savedPost.UserId = userId;

        var res = await _repository.CreateSavedPost(savedPost);

        return res;
    }
}
