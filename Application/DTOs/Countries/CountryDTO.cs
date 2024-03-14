using Application.DTOs.Common;

namespace Application.DTOs.Countries;

public class CountryDTO : BaseAuditableDTO
{
    public string Name { get; set; }
    public string CountryCode { get; set; }
}
