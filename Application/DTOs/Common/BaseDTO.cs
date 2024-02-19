using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Common;

public class BaseDTO : BaseAuditableDTO
{
    public DateTimeOffset Created { get; set; }

    public DateTimeOffset LastModified { get; set; }
}
