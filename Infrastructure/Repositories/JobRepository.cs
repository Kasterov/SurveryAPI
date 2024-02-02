using Application.Abstractions.Common;
using Application.Abstractions.Jobs;
using Application.DTOs.Jobs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class JobRepository : IJobRepository
{
    private readonly IApplicationDbContext _context;
    public JobRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Job>> GetJobList()
    {
        return await _context.Jobs.ToListAsync();
    }
}
