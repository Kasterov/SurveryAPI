using Domain.Common;

namespace Domain.Entities;

public class Post : BaseAuditableEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsMultiple { get; set; }
    public int AuthorId { get; set; }
    public User Author { get; set; }
    public List<PoolOption> Options { get; set; }
}
