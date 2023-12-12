using Domain.Enums;

namespace Application.DTOs.Users;

public class CreateUserDTO
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
}
