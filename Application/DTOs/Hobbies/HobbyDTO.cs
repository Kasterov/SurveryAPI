using Application.DTOs.Common;

namespace Application.DTOs.Hobbies;

public class HobbyDTO : BaseAuditableDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
}
