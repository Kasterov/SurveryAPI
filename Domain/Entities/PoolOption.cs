using Domain.Common;

namespace Domain.Entities;

public class PoolOption : BaseAuditableEntity
{
    public string Title { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public List<Vote> Votes { get; set; }
}
