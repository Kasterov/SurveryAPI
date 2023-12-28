using Application.DTOs.Common;

namespace Application.DTOs.Votes;

public class VoteLiteDTO : BaseDTO
{
    public string Option { get; set; }
    public int Count { get; set; }
}
