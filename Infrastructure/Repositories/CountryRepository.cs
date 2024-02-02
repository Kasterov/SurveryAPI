using Application.Abstractions.Common;
using Application.Abstractions.Countries;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly IApplicationDbContext _context;
    public CountryRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Country>> GetCountrytList()
    {
        return await _context.Countries.ToListAsync();
    }
}
