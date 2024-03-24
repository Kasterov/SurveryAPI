using Application.Abstractions.Common;
using Application.Abstractions.Complains;
using Application.DTOs.Complains;
using Application.DTOs.Posts;
using Application.DTOs.Users;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ComplainRepository : IComplainRepository
{
    private readonly IApplicationDbContext _context;

    public ComplainRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CreateComplain(Complain complain)
    {
        var entity = _context.Complains.Add(complain);
        await _context.SaveChangesAsync();

        if (entity is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<GetComplainDTO> GetComplain(int id)
    {
        var res = await _context.Complains.Where(x => x.Id == id)
            .Select(u => new GetComplainDTO
            {
                Id = u.Id,
                Comment = u.Comment,
                ComplainReason = u.ComplainReason,
                Created = u.Created,
                User = new UserLinkDTO
                {
                    Id = u.User.Id,
                    Name = u.User.Name,
                    AvatarId = u.User.AvatarId
                },
                Post = new PostLinkDTO
                {
                    Id = u.Post.Id,
                    Title = u.Post.Title
                }
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return res;
    }

    public async Task<IEnumerable<GetComplainDTO>> GetComplainList()
    {
        var res = await _context.Complains
            .Select(u => new GetComplainDTO
            {
                Id = u.Id,
                Comment = u.Comment,
                ComplainReason = u.ComplainReason,
                Created = u.Created,
                User = new UserLinkDTO
                {
                    Id = u.User.Id,
                    Name = u.User.Name,
                    AvatarId = u.User.AvatarId
                },
                Post = new PostLinkDTO
                {
                    Id = u.Post.Id,
                    Title = u.Post.Title
                }
            })
            .AsNoTracking()
            .ToListAsync();

        return res;
    }
}
