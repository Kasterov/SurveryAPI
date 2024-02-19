using Application.Abstractions.Common;
using Application.Abstractions.Votes;
using Application.DTOs.Votes;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.Repositories;

public class VoteRepository : IVoteRepository
{
    private readonly IApplicationDbContext _context;
    public VoteRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Vote> Add(Vote vote)
    {
        var result = await _context.Votes.AddAsync(vote);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<IEnumerable<Vote>> AddList(IEnumerable<Vote> voteList)
    {
        var userId = voteList.First().UserId;

        var poolOptionIdsForPosts = await _context.PoolOptions
            .Where(v => voteList.Select(vote => vote.PoolOptionId).Contains(v.Id))
            .SelectMany(v => v.Post.Options.Select(option => option.Id))
            .Distinct()
            .ToListAsync();

        var voteListToDelete = await _context.Votes
            .Where(vote => vote.UserId == userId && poolOptionIdsForPosts != null && poolOptionIdsForPosts.Contains(vote.PoolOptionId))
            .ToListAsync();

        _context.Votes.RemoveRange(voteListToDelete);

        _context.Votes.AddRange(voteList);
        await _context.SaveChangesAsync();

        return voteList;
    }

    public async Task<Vote> Delete(int id)
    {
        var voteToDelete = await _context.Votes.FirstOrDefaultAsync(ass => ass.Id == id);

        if (voteToDelete is null)
        {
            throw new NullReferenceException();
        }

        var result = _context.Votes.Remove(voteToDelete);

        if (result is null)
        {
            throw new Exception();
        }

        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public async Task<IEnumerable<VotedPostDTO>> GetVotesForPost(int userId, int postId)
    {
        var postOptions = await _context.Posts
            .Include(post => post.Options)
            .Where(p => p.Id == postId)
            .Select(post => post.Options.Select(x => x.Id))
            .FirstOrDefaultAsync();

        return await _context.Votes
            .Where(v => v.UserId == userId &&
            postOptions != null &&
            postOptions.Contains(v.PoolOptionId))
            .Select(v => new VotedPostDTO()
            {
                PoolId = v.PoolOptionId,
            })
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Vote> GetById(int id)
    {
        var vote = await _context.Votes
           .Include(vote => vote.User)
           .AsNoTracking()
           .FirstOrDefaultAsync(ass => ass.Id == id);

        return vote;
    }

    public Task<Vote> Update(Vote vote)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> RemoveList(int userId, int postId)
    {
        var poolOptionIdList = await _context.Posts
            .Select(p => new { 
                p.Id,
                PoolOptionId = p.Options.Select(x => x.Id),
            })
            .FirstOrDefaultAsync(p => p.Id == postId);

        var voteListToDelete = await _context.Votes
            .Where(vote => vote.UserId == userId && poolOptionIdList != null && poolOptionIdList.PoolOptionId.Contains(vote.PoolOptionId))
            .ToListAsync();

        if (voteListToDelete.IsNullOrEmpty())
        {
            return true;
        }

        _context.Votes.RemoveRange(voteListToDelete);
        int saved = await _context.SaveChangesAsync();
        return saved != 0;
    }
}
