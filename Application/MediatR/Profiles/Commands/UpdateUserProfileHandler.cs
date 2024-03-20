using Application.Abstractions.Posts;
using Application.Abstractions.Users;
using Application.DTOs.Posts;
using Application.DTOs.Profiles;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.Profiles.Commands;

public record UpdateUserProfile(UserProfileUpdateDTO dto) : IRequest<bool>;

public class UpdateUserProfileHandler : IRequestHandler<UpdateUserProfile, bool>
{
    private readonly IUserRepository _repository;
    private readonly IMapper _mapper;
    private readonly Abstractions.Users.IIdentity _iIdentity;
    public UpdateUserProfileHandler(
        IUserRepository repository,
        IMapper mapper,
        Abstractions.Users.IIdentity identity)
    {
        _repository = repository;
        _mapper = mapper;
        _iIdentity = identity;
    }

    public async Task<bool> Handle(UpdateUserProfile request, CancellationToken cancellationToken)
    {
        Media? file = null;

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

        var userProfile = new User()
        {
            Id = Convert.ToInt32(_iIdentity.UserId),
            Gender = request.dto.Gender,
            Relationship = request.dto.Relationship,
            SpendingPerMonth = request.dto.SpendingPerMonth,
            CountryId = request.dto.CountryId,
            Bio = request.dto.Bio,
            Avatar = file,
            Name = request.dto.Name,
            Email = request.dto.Email,
            DateOfBirth = request.dto.DateOfBirth,
            Jobs = request.dto.Jobs.Select(j => new UserJob()
            {
                JobId = j.JobId,
            }).ToList(),
            Hobbies = request.dto.Hobbies.Select(j => new UserHobby()
            {
                HobbyId = j.HobbyId,
            }).ToList(),
            Educations = request.dto.Educations.Select(j => new UserEducation()
            {
                EducationId = j.EducationId
            }).ToList()
        };

        var res = await _repository.UpdateUserProfile(userProfile);

        if (res is not null)
        {
            return true;
        }

        return false;
    }
}
