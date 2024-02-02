using Application.DTOs.Jobs;
using Domain.Entities;

namespace Application.Abstractions.Jobs;

public interface IJobRepository
{
    public Task<IEnumerable<Job>> GetJobList();
}
