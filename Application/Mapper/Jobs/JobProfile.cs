using Application.DTOs.Jobs;
using Application.DTOs.PoolOptions;
using Domain.Entities;

namespace Application.Mapper.Jobs;

public class JobProfile : AutoMapper.Profile
{
    public JobProfile()
    {
        CreateMap<JobDTO, Job>().ReverseMap();
    }
}