using Application.Abstractions.Posts;
using Application.Abstractions.Profiles;
using Application.Abstractions.Users;
using Application.DTOs.Posts;
using Application.DTOs.Profiles;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using System.Security.Principal;
using System.Xml.Linq;

namespace Application.MediatR.Profiles.Commands;

public record CreateProfile(ProfileUpdateDTO dto) : IRequest<ProfileDTO>;

public class CreateProfileHandler : IRequestHandler<CreateProfile, ProfileDTO>
{
    private readonly IProfileRepository _repository;
    private readonly IMapper _mapper;
    private readonly Abstractions.Users.IIdentity _iIdentity;
    public CreateProfileHandler(
        IProfileRepository repository,
        IMapper mapper,
        Abstractions.Users.IIdentity identity)
    {
        _repository = repository;
        _mapper = mapper;
        _iIdentity = identity;
    }

    public async Task<ProfileDTO> Handle(CreateProfile request, CancellationToken cancellationToken)
    {
        FileEntity? file = null;

        if (request.dto.Avatar is not null)
        {
            file = new()
            {
                Id = request.dto.Avatar.Id ?? 0,
                ContentType = request.dto.Avatar.ContentType,
                Bytes = System.Convert.FromBase64String(request.dto.Avatar.Base64),
                Name = request.dto.Avatar.Name,
                Expression = request.dto.Avatar.Expression,
            };
        }

        var profile = new Domain.Entities.Profile()
        {
            Gender = request.dto.Gender,
            Relationship = request.dto.Relationship,
            Sallary = request.dto.Sallary,
            CountryId = request.dto.CountryId,
            Bio = request.dto.Bio,
            FileEntity = file,
            UserId = Convert.ToInt32(_iIdentity.UserId),
            User = new User()
            {
                Name = request.dto.Name,
                Email = request.dto.Email,
                DateOfBirth = request.dto.DateOfBirth
            },
            Jobs = request.dto.Jobs.Select(j => new ProfileJob()
            {
                JobId = j.JobId,
            }).ToList(),
            Hobbies = request.dto.Hobbies.Select(j => new ProfileHobby()
            {
                HobbyId = j.HobbyId,
            }).ToList(),
            Educations = request.dto.Educations.Select(j => new ProfileEducation()
            {
                EducationId = j.EducationId
            }).ToList()
        };

        var res = await _repository.AddOrUpdate(profile);

        return new ProfileDTO();
    }
}
