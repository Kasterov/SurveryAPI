using Application.DTOs.PoolOptions;
using Domain.Entities;

namespace Application.Mapper.PoolOptions;

public class PoolOptionProifle : AutoMapper.Profile
{
    public PoolOptionProifle()
    {
        CreateMap<PoolOptionDTO, PoolOption>().ReverseMap();
        CreateMap<PoolOption, PoolOptionBaseDTO>().ReverseMap();
        CreateMap<Post, PoolOptionsForVoteDTO>().ReverseMap();
    }
}
