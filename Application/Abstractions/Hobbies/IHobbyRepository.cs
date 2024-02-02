using Domain.Entities;

namespace Application.Abstractions.Hobbies;

public interface IHobbyRepository
{
    public Task<IEnumerable<Hobby>> GetHobbyList(); 
}
