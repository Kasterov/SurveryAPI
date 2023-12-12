using Application.Abstractions.Common;
using Application.Abstractions.Votes;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

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

    public Task<IEnumerable<Vote>> GetAll()
    {
        throw new NotImplementedException();
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
}
