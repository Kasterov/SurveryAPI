using Application.DTOs.Profiles;
using AutoMapper;
using Domain.Entities;

namespace Application.Abstractions.Users;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAll();
    public Task<User> GetById(int id);
    public Task<User> GetByName(string Name);
    public Task<User> Add(User user);
    public Task<User> Delete(int id);
    public Task<User> UpdateUserProfile(User profile);
    public Task<ProfileViewDTO> GetUserProfileViewDTO(int userId);
    public Task<User?> GetProfileByUserId(int userId);
    public Task<bool> PatchActivateUser(int userId);
}
