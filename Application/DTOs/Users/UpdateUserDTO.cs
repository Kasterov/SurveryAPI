using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.Users;

public class UpdateUserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string OldPassword { get; set; }
    public string NewPasswroed { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}
