using Application.Abstractions.Files;
using Application.Abstractions.Users;
using Application.DTOs.Countries;
using Application.DTOs.Educations;
using Application.DTOs.Hobbies;
using Application.DTOs.Jobs;
using Application.DTOs.Profiles;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.MediatR.Profiles.Queries;

public record GetProfileViewData(int Id) : IRequest<ProfileViewDTO?>;

public class GetProfileViewDataHandler : IRequestHandler<GetProfileViewData, ProfileViewDTO?>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IMediaLinkGeneratorService _mediaLinkGeneratorService;

    public GetProfileViewDataHandler(
        IUserRepository userRepository,
        IMapper mapper,
        IMediaLinkGeneratorService mediaLinkGeneratorService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _mediaLinkGeneratorService = mediaLinkGeneratorService;
    }

    public async Task<ProfileViewDTO?> Handle(GetProfileViewData request, CancellationToken cancellationToken)
    {
        var profileView = await _userRepository.GetUserProfileViewDTO(request.Id);

        profileView.AvatarLink = _mediaLinkGeneratorService.GenerateMediaLink(profileView.AvatarId);

        return profileView;
    }
}