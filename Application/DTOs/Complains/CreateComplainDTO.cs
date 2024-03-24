using Domain.Enums;

namespace Application.DTOs.Complains;

public class CreateComplainDTO
{
    public int PostId { get; set; }
    public string Comment { get; set; }
    public ComplainReason ComplainReason { get; set; }
}
