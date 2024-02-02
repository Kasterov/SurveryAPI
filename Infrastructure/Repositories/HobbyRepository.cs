using Application.Abstractions.Common;
using Application.Abstractions.Hobbies;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class HobbyRepository : IHobbyRepository
{
    private readonly IApplicationDbContext _context;
    public HobbyRepository(IApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Hobby>> GetHobbyList()
    {
        return await _context.Hobbies.ToListAsync();
    }
}
