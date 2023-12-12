using Application.DTOs.Common;
using Application.DTOs.Posts;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.Users;

public class UserDTO : BaseDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public IEnumerable<PostDTO> Posts { get; set; }
}
