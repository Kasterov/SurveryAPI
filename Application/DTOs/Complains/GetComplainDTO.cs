using Application.DTOs.Common;
using Application.DTOs.Posts;
using Application.DTOs.Users;
using Domain.Enums;

namespace Application.DTOs.Complains;

public class GetComplainDTO : BaseAuditableDTO
{
    public string Comment { get; set; }
    public PostLinkDTO Post { get; set; }
    public UserLinkDTO User { get; set; }
    public ComplainReason ComplainReason { get; set; }
    public DateTimeOffset Created { get; set; }
}