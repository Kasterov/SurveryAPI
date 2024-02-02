using Domain.Common;

namespace Domain.Entities;

public class Country : BaseAuditableEntity
{
    public string Name { get; set; }
    public string CountryCode { get; set; }
    public Profile Profile { get; set; }
}
