using Application.DTOs.Educations;
using Application.DTOs.Hobbies;
using Domain.Entities;

namespace Application.Mapper.Educations;

public class EducationProfile : AutoMapper.Profile
{
    public EducationProfile()
    {
        CreateMap<EducationDTO, Education>().ReverseMap();
    }
}