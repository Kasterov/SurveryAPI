using Application.DTOs.Votes;

namespace Application.DTOs.Posts;

public class PostLiteDTO
{
    public string Title { get; set; }
    public int AuthorId { get; set; }
    public IEnumerable<VoteLiteDTO> Votes { get; set; }
}
