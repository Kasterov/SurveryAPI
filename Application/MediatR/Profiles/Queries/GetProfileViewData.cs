using Application.Abstractions.Files;
using Application.Abstractions.Profiles;
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
    private readonly IProfileRepository _profileRepository;
    private readonly IMapper _mapper;
    private readonly IMediaLinkGeneratorService _mediaLinkGeneratorService;

    public GetProfileViewDataHandler(
        IProfileRepository profileRepository,
        IMapper mapper,
        IMediaLinkGeneratorService mediaLinkGeneratorService)
    {
        _profileRepository = profileRepository;
        _mapper = mapper;
        _mediaLinkGeneratorService = mediaLinkGeneratorService;
    }

    public async Task<ProfileViewDTO?> Handle(GetProfileViewData request, CancellationToken cancellationToken)
    {
        var profileView = await _profileRepository.GetProfileViewDTO(request.Id);

        profileView.FileEntityLink = _mediaLinkGeneratorService.GenerateMediaLink(profileView.FileEntityId);

        return profileView;
    }
}