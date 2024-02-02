using Application.Abstractions.Profiles;
using Application.Abstractions.Users;
using Application.DTOs.Profiles;
using Application.DTOs.Users;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Profiles.Queries;

public record GetProfileByUserId() : IRequest<ProfileToUpdateDTO?>;

public class GetProfileByUserIdHandler : IRequestHandler<GetProfileByUserId, ProfileToUpdateDTO?>
{
    private readonly IProfileRepository _repository;
    private readonly IMapper _mapper;
    private readonly IIdentity _identity;

    public GetProfileByUserIdHandler(IProfileRepository repository, IMapper mapper, IIdentity identity)
    {
        _repository = repository;
        _mapper = mapper;
        _identity = identity;
    }

    public async Task<ProfileToUpdateDTO?> Handle(GetProfileByUserId request, CancellationToken cancellationToken)
    {
        var profile = await _repository.GetProfileByUserId(Convert.ToInt32(_identity.UserId));

        return profile;
    }
}
