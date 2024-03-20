using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Education : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public EducationType EducationType { get; set; }
    public List<UserEducation> ProfileEducations { get; set; }
}
