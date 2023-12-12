using Domain.Common;
using Domain.Enums;
using System;

namespace Domain.Entities;

public class User : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public Gender Gender { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<Post> Posts { get; set; }
    public List<Vote> Votes { get; set; }
}
