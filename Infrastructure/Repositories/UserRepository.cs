using Application.Abstractions.Common;
using Application.Abstractions.Users;
using Application.DTOs.Countries;
using Application.DTOs.Educations;
using Application.DTOs.Hobbies;
using Application.DTOs.Jobs;
using Application.DTOs.Profiles;
using AutoMapper;
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

    public Task<User> AddOrUpdateUserProfile(UserProfileUpdateDTO profile)
    {
        throw new NotImplementedException();
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

    public async Task<User?> UpdateUserProfile(User profile)
    {
        User? existProfile = await _context.Users
           .Include(x => x.Educations)
           .Include(x => x.Hobbies)
           .Include(x => x.Jobs)
           .FirstOrDefaultAsync(p => p.Id == profile.Id);

        if (existProfile is null)
        {
            return null;
        }

        existProfile.Name = profile.Name;
        existProfile.DateOfBirth = profile.DateOfBirth;
        existProfile.Bio = profile.Bio;
        existProfile.Avatar = profile.Avatar;
        existProfile.Email = profile.Email;
        existProfile.CountryId = profile.CountryId;
        existProfile.Educations = profile.Educations;
        existProfile.Hobbies = profile.Hobbies;
        existProfile.Jobs = profile.Jobs;
        existProfile.SpendingPerMonth = profile.SpendingPerMonth;
        existProfile.Gender = profile.Gender;
        existProfile.Relationship = profile.Relationship;

        var result = _context.Users.Update(existProfile).Entity;
        await _context.SaveChangesAsync();

        return result;

    }

    public async Task<User?> GetProfileByUserId(int userId)
    {
        var profile = await _context.Users
            .Include(p => p.Country)
            .Include(p => p.Educations)
            .ThenInclude(e => e.Education)
            .Include(p => p.Jobs)
            .ThenInclude(p => p.Job)
            .Include(p => p.Hobbies)
            .ThenInclude(p => p.Hobby)
            .FirstOrDefaultAsync(p => p.Id == userId);

        return profile;
    }

    public async Task<ProfileViewDTO> GetUserProfileViewDTO(int userId)
    {
        var profile = await _context.Users
            .Where(x => x.Id == userId)
            .Select(u => new ProfileViewDTO
            {
                AvatarId = u.AvatarId,
                Name = u.Name,
                Bio = u.Bio,
                RegistrationDate = u.Created,
                Country = new CountryDTO()
                {
                    Id = u.Country.Id,
                    CountryCode = u.Country.CountryCode,
                    Name = u.Country.Name
                },
                Educations = u.Educations.Select(p => new EducationDTO()
                {
                    Id = p.EducationId,
                    Name = p.Education.Name,
                    Description = p.Education.Description
                }),
                Jobs = u.Jobs.Select(p => new JobDTO()
                {
                    Id = p.Job.Id,
                    Name = p.Job.Name,
                    Description = p.Job.Description
                }),
                Hobbies = u.Hobbies.Select(p => new HobbyDTO()
                {
                    Id = p.Hobby.Id,
                    Name = p.Hobby.Name,
                    Description = p.Hobby.Description
                })
            })
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return profile;
    }
}
