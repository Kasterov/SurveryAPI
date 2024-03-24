using Application.Abstractions.Complains;
using Application.Abstractions.Files;
using Application.DTOs.Complains;
using MediatR;

namespace Application.MediatR.Complains.Queries;

public record GetComplainById(int Id) : IRequest<GetComplainDTO>;

public class GetComplainByIdHandler : IRequestHandler<GetComplainById, GetComplainDTO>
{
    private readonly IComplainRepository _complainRepository;
    private readonly IMediaLinkGeneratorService _mediaLinkGeneratorService;

    public GetComplainByIdHandler(IComplainRepository complainRepository, IMediaLinkGeneratorService mediaLinkGeneratorService)
    {
        _complainRepository = complainRepository;
        _mediaLinkGeneratorService = mediaLinkGeneratorService;
    }

    public async Task<GetComplainDTO> Handle(GetComplainById request, CancellationToken cancellationToken)
    {
        var complain = await _complainRepository.GetComplain(request.Id);

        if (complain.User.AvatarId is not null)
        {
            complain.User.AvatarLink = _mediaLinkGeneratorService.GenerateMediaLink(complain.User.AvatarId);
        }

        return complain;
    }
}
