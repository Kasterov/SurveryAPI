using Application.DTOs.Common;

namespace Application.DTOs.Posts;

public class PostLinkDTO : BaseAuditableDTO
{
    public string Title { get; set; }
}