using Application.Abstractions.Common;
using Application.Abstractions.Users;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IApplicationDbContext _context;
    public UserRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> Add(User user)
    {
        var result = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return result.Entity;
    }

    public Task<User> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetById(int id)
    {
        var assignment = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(ass => ass.Id == id);

        return assignment;
    }

    public Task<User> Update(User assignment)
    {
        throw new NotImplementedException();
    }
}
