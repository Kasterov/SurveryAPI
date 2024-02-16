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
using Profile = Domain.Entities.Profile;

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
        existProfile.FileEntity = profile.FileEntity;

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

    public async Task<Profile?> GetProfileByUserId(int userId)
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

        return profile;
    }
}
