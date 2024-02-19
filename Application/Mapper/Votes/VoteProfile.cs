using Application.DTOs.Users;
using Application.DTOs.Votes;
using Domain.Entities;

namespace Application.Mapper.Votes;

public class VoteProfile : AutoMapper.Profile
{
    public VoteProfile()
    {
        CreateMap<VoteDTO, Vote>().ReverseMap();
    }
}