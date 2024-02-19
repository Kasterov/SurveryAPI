using Application.DTOs.Common;

namespace Application.DTOs.Votes;

public class VoteLiteDTO : BaseAuditableDTO
{
    public string Option { get; set; }
    public int Count { get; set; }
}
