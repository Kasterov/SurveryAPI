using Application.DTOs.Hobbies;
using Application.DTOs.Jobs;
using Application.DTOs.PoolOptions;
using Domain.Entities;

namespace Application.Mapper.Jobs;

public class JobProfile : AutoMapper.Profile
{
    public JobProfile()
    {
        CreateMap<JobDTO, Job>().ReverseMap();
        CreateMap<UserJob, UploadJobDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Job.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.JobId));
    }
}