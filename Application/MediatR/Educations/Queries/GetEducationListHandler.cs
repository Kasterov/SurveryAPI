using Application.Abstractions.Educations;
using Application.DTOs.Educations;
using Application.DTOs.Jobs;
using Application.DTOs.PoolOptions;
using MediatR;
using System.Collections.Generic;

namespace Application.MediatR.Educations.Queries;

public record GetEducationList() : IRequest<IEnumerable<EducationDTO>>;

public class GetEducationListHandler : IRequestHandler<GetEducationList, IEnumerable<EducationDTO>>
{
    private readonly IEducationRepository _educationRepository;

    public GetEducationListHandler(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
    }

    public async Task<IEnumerable<EducationDTO>> Handle(GetEducationList request, CancellationToken cancellationToken)
    {
        var educationList = await _educationRepository.GetEducationtList();

        return educationList.Select(j => new EducationDTO()
        {
            Id = j.Id,
            Description = j.Description,
            Name = j.Name
        });
    }
}
