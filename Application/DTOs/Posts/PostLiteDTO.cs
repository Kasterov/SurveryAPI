using Application.DTOs.Common;
using Application.DTOs.Users;
using Application.DTOs.Votes;

namespace Application.DTOs.Posts;

public class PostLiteDTO : BaseDTO
{
    public string Title { get; set; }
    public AuthorDTO Author { get; set; }
    public int TotalCount { get; set; }
    public bool IsMultiple { get; set; }
    public IEnumerable<VoteLiteDTO> Votes { get; set; }
}

public class AuthorDTO : BaseAuditableDTO
{
    public string Name { get; set;}
    public string AvatarLink { get; set; }
    public int? AvatarId { get; set; }
}