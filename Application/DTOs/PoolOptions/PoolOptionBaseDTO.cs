using Application.DTOs.Common;

namespace Application.DTOs.PoolOptions;

public class PoolOptionBaseDTO : BaseAuditableDTO
{
    public string Title { get; set; }
    public bool? IsSelected { get; set; }

}
