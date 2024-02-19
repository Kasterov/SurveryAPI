using Application.DTOs.Votes;
using Domain.Entities;
using System.Collections.Generic;

namespace Application.Abstractions.Votes;

public interface IVoteRepository
{
    public Task<IEnumerable<VotedPostDTO>> GetVotesForPost(int userId, int postId);

    public Task<Vote> GetById(int id);

    public Task<Vote> Add(Vote vote);

    public Task<bool> RemoveList(int userId, int postId);

    public Task<IEnumerable<Vote>> AddList(IEnumerable<Vote> voteList);

    public Task<Vote> Update(Vote vote);

    public Task<Vote> Delete(int id);
}
