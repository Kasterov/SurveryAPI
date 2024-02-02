using Application.Abstractions.Common;
using Application.Abstractions.Educations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EducationRepository : IEducationRepository
{
    private readonly IApplicationDbContext _context;
    public EducationRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Education>> GetEducationtList()
    {
        return await _context.Educations.ToListAsync();
    }
}
