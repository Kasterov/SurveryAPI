using Domain.Common;
using System.Collections.Generic;

namespace Domain.Entities;

public class Country : BaseAuditableEntity
{
    public string Name { get; set; }
    public string CountryCode { get; set; }
    public IEnumerable<User> Users { get; set; }
}
