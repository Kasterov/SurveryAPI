using Application.DTOs.Common;

namespace Application.DTOs.Users;

public class UserLinkDTO : BaseAuditableDTO
{
    public string Name { get; set; }
    public int? AvatarId { get; set; }
    public string AvatarLink { get; set; }
}
