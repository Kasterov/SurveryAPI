using Application.Abstractions.Common;
using Application.Abstractions.Posts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private readonly IApplicationDbContext _context;
    public PostRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Post> Add(Post post)
    {
        var result = await _context.Posts.AddAsync(post);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<Post> Delete(int id)
    {
        var postToDelete = await _context.Posts.FirstOrDefaultAsync(ass => ass.Id == id);

        if (postToDelete is null)
        {
            throw new NullReferenceException();
        }

        var result = _context.Posts.Remove(postToDelete);

        if (result is null)
        {
            throw new Exception();
        }

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public Task<IEnumerable<Post>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<Post> GetById(int id)
    {
        var post = await _context.Posts
           .Include(post => post.Author)
           .Include(post => post.Options).ThenInclude(opt => opt.Votes)
           .AsNoTracking()
           .FirstOrDefaultAsync(ass => ass.Id == id);

        return post;
    }

    public Task<Post> Update(Post assignment)
    {
        throw new NotImplementedException();
    }
}
