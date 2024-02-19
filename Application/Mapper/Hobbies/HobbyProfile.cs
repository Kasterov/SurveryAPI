using Application.DTOs.Hobbies;
using Application.DTOs.Jobs;
using Domain.Entities;

namespace Application.Mapper.Hobbies;

public class HobbyProfile : AutoMapper.Profile
{
    public HobbyProfile()
    {
        CreateMap<HobbyDTO, Hobby>().ReverseMap();
    }
}
