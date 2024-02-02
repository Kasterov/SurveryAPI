using Application.Abstractions.Countries;
using Application.Abstractions.Educations;
using Application.DTOs.Countries;
using Application.DTOs.Educations;
using MediatR;

namespace Application.MediatR.Countries.Queries;

public record GetCountryList() : IRequest<IEnumerable<CountryDTO>>;

public class GetCountryListHandler : IRequestHandler<GetCountryList, IEnumerable<CountryDTO>>
{
    private readonly ICountryRepository _countryRepository;

    public GetCountryListHandler(ICountryRepository educationRepository)
    {
        _countryRepository = educationRepository;
    }

    public async Task<IEnumerable<CountryDTO>> Handle(GetCountryList request, CancellationToken cancellationToken)
    {
        var countryList = await _countryRepository.GetCountrytList();

        return countryList.Select(j => new CountryDTO()
        {
            Id = j.Id,
            CountryCode = j.CountryCode,
            Name = j.Name
        });
    }
}
