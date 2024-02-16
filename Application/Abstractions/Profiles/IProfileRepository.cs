using Application.DTOs.Profiles;
using Domain.Entities;

namespace Application.Abstractions.Profiles;

public interface IProfileRepository
{
    public Task<Profile> AddOrUpdate(Profile profile);

    public Task<Profile?> GetProfileByUserId(int userId);
}
