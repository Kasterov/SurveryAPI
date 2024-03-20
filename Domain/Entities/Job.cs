using Domain.Common;

namespace Domain.Entities;

public class Job : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<UserJob> ProfileJobs { get; set; }
}
