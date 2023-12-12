using System.ComponentModel.DataAnnotations;

namespace Application.DTOs.Common;

public class BaseDTO
{
    public int Id { get; set; } 

    public DateTimeOffset Created { get; set; }

    public DateTimeOffset LastModified { get; set; }
}
