using Application.Abstractions.Jobs;
using Application.Abstractions.Posts;
using Application.DTOs.Jobs;
using Application.DTOs.Posts;
using Application.DTOs.Votes;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.MediatR.Jobs.Queries;

public record GetJobList() : IRequest<IEnumerable<JobDTO>>;

public class GetJobListHandler : IRequestHandler<GetJobList, IEnumerable<JobDTO>>
{
    private readonly IJobRepository _repository;
    private readonly IMapper _mapper;

    public GetJobListHandler(IJobRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<JobDTO>> Handle(GetJobList request, CancellationToken cancellationToken)
    {
        var jobList = await _repository.GetJobList();

        return jobList.Select(j => new JobDTO()
        {
            Id = j.Id,
            Description = j.Description,
            Name = j.Name
        });
    }
}