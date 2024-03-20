using Application.DTOs.Educations;
using Application.DTOs.Hobbies;
using Domain.Entities;

namespace Application.Mapper.Educations;

public class EducationProfile : AutoMapper.Profile
{
    public EducationProfile()
    {
        CreateMap<EducationDTO, Education>().ReverseMap();
        CreateMap<UserEducation, UploadEducationDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Education.Name))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.EducationId));
    }
}