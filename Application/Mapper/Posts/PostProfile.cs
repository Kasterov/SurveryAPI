using Application.DTOs.Posts;
using Application.DTOs.Users;
using Domain.Entities;

namespace Application.Mapper.Posts;

public class PostProfile : AutoMapper.Profile
{
    public PostProfile()
    {
        CreateMap<Post, PostDTO>().ReverseMap();
    }
}
