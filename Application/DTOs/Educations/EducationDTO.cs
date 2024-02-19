using Application.DTOs.Common;

namespace Application.DTOs.Educations;

public class EducationDTO : BaseAuditableDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
}