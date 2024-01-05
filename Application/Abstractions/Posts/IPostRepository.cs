using Application.DTOs.Posts;
using Domain.Entities;

namespace Application.Abstractions.Posts;

public interface IPostRepository
{
    public Task<IEnumerable<PostLiteDTO>> GetAll();

    public Task<IEnumerable<PostTableFullDTO>> GetTablePostList(int UserId);

    public Task<Post> GetById(int id);

    public Task<Post> Add(Post post);

    public Task<Post> Update(Post post);

    public Task<Post> Delete(int id);
}
