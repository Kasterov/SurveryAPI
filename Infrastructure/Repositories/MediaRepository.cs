using Application.Abstractions.Common;
using Application.Abstractions.Files;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MediaRepository : IMediaRepository
{
    private readonly IApplicationDbContext _context;
    public MediaRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Media?> GetFile(int id)
    {
        var res = await _context.Files.FirstOrDefaultAsync(f => f.Id == id);

        return res;
    }

    public async Task<bool> UploadFile(Media file)
    {
        var res = _context.Files.Add(file);
        await _context.SaveChangesAsync();

        if (res.Entity != null)
        {
            return true;
        }

        return false;
    }
}
