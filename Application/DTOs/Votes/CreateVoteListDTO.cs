namespace Application.DTOs.Votes;

public class CreateVoteListDTO
{
    public int UserId { get; set; }
    public IEnumerable<int> VoteIdList { get; set; }
}
