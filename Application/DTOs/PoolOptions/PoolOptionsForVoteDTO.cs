using Application.DTOs.Common;
using Domain.Common;

namespace Application.DTOs.PoolOptions;

public class PoolOptionsForVoteDTO : BaseAuditableDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsMultiple { get; set; }
    public bool IsRevotable { get; set; }
    public IEnumerable<PoolOptionBaseDTO> Options { get; set; }
}
