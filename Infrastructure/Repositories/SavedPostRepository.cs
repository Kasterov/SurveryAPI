using Application.Abstractions.Common;
using Application.Abstractions.SavedPosts;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SavedPostRepository : ISavedPostRepository
{
    private readonly IApplicationDbContext _context;

    public SavedPostRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateSavedPost(SavedPost savedPost)
    {
        var res = _context.SavedPosts.Add(savedPost);
        await _context.SaveChangesAsync();

        if (res.Entity is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteSavedPost(SavedPost savedPost)
    {
        if (savedPost is null)
        {
            return false;
        }

        var res = _context.SavedPosts.Remove(savedPost);
        await _context.SaveChangesAsync();

        if (res.Entity is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> IsPostSaved(int postId, int userId)
    {
        var record = await _context.SavedPosts.FirstOrDefaultAsync(x => x.PostId == postId && x.UserId == userId);

        if (record is not null)
        {
            return true;
        }

        return false;
    }
}
