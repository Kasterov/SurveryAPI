using Application.Abstractions.Common;
using Application.Abstractions.UserCodes;
using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserCodeRepository : IUserCodeRepository
{
    private readonly IApplicationDbContext _context;

    public UserCodeRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Add(UserCode userCode)
    {
        var res = _context.UserCodes.Add(userCode);
        await _context.SaveChangesAsync();

        if (res.Entity is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<bool> DeleteByUserIdAndType(UserCode userCode)
    {
        var res = _context.UserCodes.Remove(userCode);
        await _context.SaveChangesAsync();

        if (res.Entity is not null)
        {
            return true;
        }

        return false;
    }

    public async Task<UserCode?> GetByUserIdAndType(string code, UserCodeType type)
    {
        var userCode = await _context.UserCodes
            .Where(ass => ass.Code == code && ass.Type == type)
            .Select(x => new UserCode()
            {
                Id = x.Id,
                UserId = x.UserId,
                Code = code
            })
            .AsNoTracking()
            .ToListAsync();

        return userCode?.FirstOrDefault();
    }
}
