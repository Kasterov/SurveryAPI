using Application.DTOs.Educations;
using Application.DTOs.Hobbies;
using Application.DTOs.Jobs;
using Domain.Entities;

namespace Application.Mapper.Hobbies;

public class HobbyProfile : AutoMapper.Profile
{
    public HobbyProfile()
    {
        CreateMap<HobbyDTO, Hobby>().ReverseMap();
        CreateMap<UserHobby, UploadHobbyDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Hobby.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.HobbyId));
    }
}
