using Application.DTOs.Countries;
using Application.DTOs.Profiles;
using Application.DTOs.Users;
using Domain.Entities;

namespace Application.Mapper.Countries;

public class CountryProfile : AutoMapper.Profile
{
    public CountryProfile()
    {
        CreateMap<Country, UploadCountryDTO>().ReverseMap();
    }
}
