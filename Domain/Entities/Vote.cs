using Domain.Common;

namespace Domain.Entities;

public class Vote : BaseAuditableEntity
{
    public int UserId { get; set; }
    public int PoolOptionId { get; set; }
    public User User { get; set; }
    public PoolOption PoolOption { get; set; }
}