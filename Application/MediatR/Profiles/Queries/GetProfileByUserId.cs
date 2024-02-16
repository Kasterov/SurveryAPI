using Application.Abstractions.Profiles;
using Application.Abstractions.Users;
using Application.DTOs.Countries;
using Application.DTOs.Educations;
using Application.DTOs.Hobbies;
using Application.DTOs.Jobs;
using Application.DTOs.Profiles;
using Application.DTOs.Users;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.MediatR.Profiles.Queries;

public record GetProfileByUserId() : IRequest<ProfileToUpdateDTO?>;

public class GetProfileByUserIdHandler : IRequestHandler<GetProfileByUserId, ProfileToUpdateDTO?>
{
    private readonly IProfileRepository _profileRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IIdentity _identity;

    public GetProfileByUserIdHandler(
        IProfileRepository profileRepository,
        IUserRepository userRepository,
        IMapper mapper,
        IIdentity identity)
    {
        _profileRepository = profileRepository;
        _userRepository = userRepository;
        _mapper = mapper;
        _identity = identity;
    }

    public async Task<ProfileToUpdateDTO?> Handle(GetProfileByUserId request, CancellationToken cancellationToken)
    {
        var profile = await _profileRepository.GetProfileByUserId(Convert.ToInt32(_identity.UserId));

        if (profile is null)
        {
            User user = await _userRepository.GetById(Convert.ToInt32(_identity.UserId));

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
            FileEntityId = profile.FileEntityId,
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

        if (profileUpdateDTO.FileEntityId is not null)
        {
            profileUpdateDTO.FileEntityLink = $"https://localhost:7213/File/file-content?id={profile.FileEntityId}";
        }

        return profileUpdateDTO;
    }
}
