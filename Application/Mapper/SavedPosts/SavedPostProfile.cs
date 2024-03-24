using Application.DTOs.SavedPosts;
using Application.MediatR.SavedPosts.Commands;
using Domain.Entities;

public class UserProfile : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<SavedPost, CreateSavedPostDTO>().ReverseMap();
        CreateMap<SavedPost, DeleteSavedPostByIdDTO>().ReverseMap();
    }
}


