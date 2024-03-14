using Application.DTOs.Common;
using Application.DTOs.Votes;

namespace Application.DTOs.Posts;

public class PostFullDTO : BaseDTO
{
    public string Title { get; set; }
    public AuthorDTO Author { get; set; }
    public string? Description { get; set; }
    public int TotalCount { get; set; }
    public bool IsMultiple { get; set; }
    public bool IsRevotable { get; set; }
    public bool IsPrivate { get; set; }
    public IEnumerable<VoteLiteDTO> Votes { get; set; }
}
