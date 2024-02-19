using Application.Abstractions.Hobbies;
using Application.DTOs.Hobbies;
using AutoMapper;
using MediatR;

namespace Application.MediatR.Hobbies.Queires;

public record GetHobbyList() : IRequest<IEnumerable<HobbyDTO>>;

public class GetHobbyListHandler : IRequestHandler<GetHobbyList, IEnumerable<HobbyDTO>>
{
    private readonly IHobbyRepository _hobbyRepository;
    private readonly IMapper _mapper;

    public GetHobbyListHandler(IHobbyRepository educationRepository, IMapper mapper)
    {
        _hobbyRepository = educationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<HobbyDTO>> Handle(GetHobbyList request, CancellationToken cancellationToken)
    {
        var educationList = await _hobbyRepository.GetHobbyList();

        return _mapper.Map<IEnumerable<HobbyDTO>>(educationList);
    }
}
