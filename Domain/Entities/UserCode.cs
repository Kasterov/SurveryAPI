using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class UserCode : BaseAuditableEntity
{
    public User User { get; set; }
    public int UserId {get;set;}
    public string Code { get; set; }
    public UserCodeType Type { get; set; }
}
