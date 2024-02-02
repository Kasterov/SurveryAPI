using Application.Abstractions.Common;
using Application.Abstractions.Profiles;
using Application.DTOs.Countries;
using Application.DTOs.Educations;
using Application.DTOs.Hobbies;
using Application.DTOs.Jobs;
using Application.DTOs.ProfileEducations;
using Application.DTOs.ProfileHobbies;
using Application.DTOs.ProfileJobs;
using Application.DTOs.Profiles;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ProfileRepository : IProfileRepository
{
    private readonly IApplicationDbContext _context;
    public ProfileRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Profile> AddOrUpdate(Domain.Entities.Profile profile)
    {
        Domain.Entities.Profile? existProfile = await _context.Profiles
            .Include(x => x.User)
            .Include(x => x.Educations)
            .Include(x => x.Hobbies)
            .Include(x => x.Jobs)
            .FirstOrDefaultAsync(p => p.UserId == profile.UserId);

        if (existProfile is null)
        {
            User userData = await _context.Users.FirstAsync(u => u.Id == profile.UserId);

            userData.Email = profile.User.Email;
            userData.Name = profile.User.Name;
            userData.DateOfBirth = profile.User.DateOfBirth;

            profile.User = userData;

            var res = (await _context.Profiles.AddAsync(profile)).Entity;
            await _context.SaveChangesAsync();

            return res;
        }

        Domain.Entities.Profile result;
        User user = profile.User;

        existProfile.Gender = profile.Gender;
        existProfile.Sallary = profile.Sallary;
        existProfile.Relationship = profile.Relationship;
        existProfile.CountryId = profile.CountryId;
        existProfile.Bio = profile.Bio;

        existProfile.Educations = profile.Educations;
        existProfile.Jobs = profile.Jobs;
        existProfile.Hobbies = profile.Hobbies;

        existProfile.User.Name = user.Name;
        existProfile.User.Email = user.Email;
        existProfile.User.DateOfBirth = user.DateOfBirth;

        result = _context.Profiles.Update(existProfile).Entity;
        await _context.SaveChangesAsync();

        return result;

    }

    public async Task<ProfileToUpdateDTO?> GetProfileByUserId(int userId)
    {
        var profile = await _context.Profiles
            .Include(p => p.Country)
            .Include(p => p.User)
            .Include(p => p.Educations)
            .ThenInclude(e => e.Education)
            .Include(p => p.Jobs)
            .ThenInclude(p => p.Job)
            .Include(p => p.Hobbies)
            .ThenInclude(p => p.Hobby)
            .FirstOrDefaultAsync(p => p.UserId == userId);

        if (profile is null)
        {
            User user = await _context.Users.FirstAsync(u => u.Id == userId);

            ProfileToUpdateDTO profileUserDTO = new()
            {
                Name = user.Name,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
            };

            return profileUserDTO;
        }

        ProfileToUpdateDTO profileUpdateDTO = new()
        {
            Id = profile.Id,
            LastModified = profile.LastModified,
            Created = profile.Created,
            Name = profile.User.Name,
            Email = profile.User.Email,
            DateOfBirth = profile.User.DateOfBirth,
            Bio = profile.Bio,
            Sallary = profile.Sallary,
            Gender = profile.Gender,
            Country = new UploadCountryDTO()
            {
                Id = profile.Country.Id,
                Name = profile.Country.Name,
            },
            Relationship = profile.Relationship,
            Educations = profile.Educations.Select(e => new UploadEducationDTO()
            {
                Id = e.EducationId,
                Name = e.Education.Name,
            }).ToList(),
            Jobs = profile.Jobs.Select(c => new UploadJobDTO()
            {
                Id = c.JobId,
                Name = c.Job.Name,
            }).ToList(),
            Hobbies = profile.Hobbies.Select(h => new UploadHobbyDTO()
            {
                Id = h.HobbyId,
                Name = h.Hobby.Name,
            }).ToList(),
        };

        return profileUpdateDTO;
    }
}
