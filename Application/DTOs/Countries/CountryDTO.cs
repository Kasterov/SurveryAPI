using Application.DTOs.Common;

namespace Application.DTOs.Countries;

public class CountryDTO : BaseDTO
{
    public string Name { get; set; }
    public string CountryCode { get; set; }
}
