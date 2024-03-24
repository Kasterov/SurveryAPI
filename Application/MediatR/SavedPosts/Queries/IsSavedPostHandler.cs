using Application.Abstractions.SavedPosts;
using Application.Abstractions.Users;
using Application.DTOs.Users;
using AutoMapper;
using MediatR;
using System.Security.Principal;

namespace Application.MediatR.SavedPosts.Queries;

public record IsSavedPost(int postId) : IRequest<bool>;
public class IsSavedPostHandler : IRequestHandler<IsSavedPost, bool>
{
    private readonly ISavedPostRepository _repository;
    private readonly Abstractions.Users.IIdentity _identity;

    public IsSavedPostHandler(
        ISavedPostRepository repository,
        Abstractions.Users.IIdentity identity)
    {
        _repository = repository;
        _identity = identity;
    }
    public async Task<bool> Handle(IsSavedPost request, CancellationToken cancellationToken)
    {
        int userId = Convert.ToInt32(_identity.UserId);

        var res = await _repository.IsPostSaved(request.postId, userId);

        return res;
    }
}
