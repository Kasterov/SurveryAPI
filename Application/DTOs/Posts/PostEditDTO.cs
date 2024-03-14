using Application.DTOs.Common;

namespace Application.DTOs.Posts;

public class PostEditDTO : BaseDTO
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public bool IsPrivate { get; set; }
    public bool IsMultiple { get; set; }
    public bool IsRevotable { get; set; }
    public IEnumerable<PoolOptionEditDTO> Options { get; set; }
}

public class PoolOptionEditDTO : BaseAuditableDTO
{
    public string Title { get; set; }
}