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
        var result = _context.Users.Update(user);
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
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(ass => ass.Id == id);

        return user;
    }

    public async Task<User> GetByName(string Name)
    {
        var user = await _context.Users
           .AsNoTracking()
           .FirstOrDefaultAsync(user => user.Name == Name);

        return user;
    }

    public Task<User> Update(User user)
    {
        throw new NotImplementedException();
    }
}
