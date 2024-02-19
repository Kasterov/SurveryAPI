namespace Application.DTOs.Votes;

public class CreateVoteListDTO
{
    public int PostId { get; set; }
    public IEnumerable<int> VoteIdList { get; set; }
}
