using Application.Abstractions.Educations;
using Application.DTOs.Educations;
using Application.DTOs.Jobs;
using Application.DTOs.PoolOptions;
using AutoMapper;
using MediatR;
using System.Collections.Generic;

namespace Application.MediatR.Educations.Queries;

public record GetEducationList() : IRequest<IEnumerable<EducationDTO>>;

public class GetEducationListHandler : IRequestHandler<GetEducationList, IEnumerable<EducationDTO>>
{
    private readonly IEducationRepository _educationRepository;
    private readonly IMapper _mapper;

    public GetEducationListHandler(IEducationRepository educationRepository, IMapper mapper)
    {
        _educationRepository = educationRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<EducationDTO>> Handle(GetEducationList request, CancellationToken cancellationToken)
    {
        var educationList = await _educationRepository.GetEducationtList();

        return _mapper.Map<IEnumerable<EducationDTO>>(educationList);
    }
}
