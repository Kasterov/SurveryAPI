using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Complain : BaseAuditableEntity
{
    public int AccuserId {  get; set; }
    public User User { get; set; }
    public int PostId { get; set; }
    public Post Post { get; set; }
    public string Comment { get; set; }
    public ComplainReason ComplainReason { get; set; }
}