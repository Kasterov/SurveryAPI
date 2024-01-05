using Domain.Entities;

namespace Application.Abstractions.Users;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAll();
    public Task<User> GetById(int id);
    public Task<User> GetByName(string Name);
    public Task<User> Add(User assignment);

    public Task<User> Update(User assignment);

    public Task<User> Delete(int id);
}
