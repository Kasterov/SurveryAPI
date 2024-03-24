using Application.DTOs.Complains;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapper.Complains;

public class CountryProfile : Profile
{
    public CountryProfile()
    {
        CreateMap<Complain, CreateComplainDTO>().ReverseMap();
    }
}
