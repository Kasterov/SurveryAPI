using Domain.Entities;

namespace Application.Abstractions.Countries;

public interface ICountryRepository
{
    public Task<IEnumerable<Country>> GetCountrytList();
}
