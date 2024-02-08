using Application.Abstractions.Common;
using Application.Abstractions.Files;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class FileEntityRepository : IFileEntityRepository
{
    private readonly IApplicationDbContext _context;
    public FileEntityRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<FileEntity?> GetFile(int id)
    {
        var res = await _context.FileEntities.FirstOrDefaultAsync(f => f.Id == id);

        return res;
    }

    public async Task<bool> UploadFile(FileEntity file)
    {
        var res = _context.FileEntities.Add(file);
        await _context.SaveChangesAsync();

        if (res.Entity != null)
        {
            return true;
        }

        return false;
    }
}
