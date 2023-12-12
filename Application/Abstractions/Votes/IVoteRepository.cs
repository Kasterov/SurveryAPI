using Domain.Entities;

namespace Application.Abstractions.Votes;

public interface IVoteRepository
{
    public Task<IEnumerable<Vote>> GetAll();

    public Task<Vote> GetById(int id);
    public Task<Vote> Add(Vote vote);

    public Task<Vote> Update(Vote vote);

    public Task<Vote> Delete(int id);
}
