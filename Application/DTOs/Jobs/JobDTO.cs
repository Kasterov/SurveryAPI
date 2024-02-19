using Application.DTOs.Common;

namespace Application.DTOs.Jobs;

public class JobDTO : BaseAuditableDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
}
