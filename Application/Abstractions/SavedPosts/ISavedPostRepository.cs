using Domain.Entities;

namespace Application.Abstractions.SavedPosts;

public interface ISavedPostRepository
{
    public Task<bool> IsPostSaved(int postId, int userId);
    public Task<bool> CreateSavedPost(SavedPost savedPost);
    public Task<bool> DeleteSavedPost(SavedPost savedPost);
}
