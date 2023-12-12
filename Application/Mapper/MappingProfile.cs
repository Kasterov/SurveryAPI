using Application.DTOs.PoolOptions;
using Application.DTOs.Posts;
using Application.DTOs.Users;
using Application.DTOs.Votes;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<Post, PostDTO>().ReverseMap();
        CreateMap<PoolOptionDTO, PoolOption>().ReverseMap();
        CreateMap<VoteDTO, Vote>().ReverseMap();
    }
}
