using Application.DTOs.Common;

namespace Application.DTOs.PoolOptions;

public class PoolOptionsForVoteDTO : BaseDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IEnumerable<PoolOptionBaseDTO> Options { get; set; }
}
