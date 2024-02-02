using Application.Abstractions.Educations;
using Application.Abstractions.Hobbies;
using Application.DTOs.Educations;
using Application.DTOs.Hobbies;
using MediatR;

namespace Application.MediatR.Hobbies.Queires;

public record GetHobbyList() : IRequest<IEnumerable<HobbyDTO>>;

public class GetHobbyListHandler : IRequestHandler<GetHobbyList, IEnumerable<HobbyDTO>>
{
    private readonly IHobbyRepository _hobbyRepository;

    public GetHobbyListHandler(IHobbyRepository educationRepository)
    {
        _hobbyRepository = educationRepository;
    }

    public async Task<IEnumerable<HobbyDTO>> Handle(GetHobbyList request, CancellationToken cancellationToken)
    {
        var educationList = await _hobbyRepository.GetHobbyList();

        return educationList.Select(j => new HobbyDTO()
        {
            Id = j.Id,
            Description = j.Description,
            Name = j.Name
        });
    }
}
