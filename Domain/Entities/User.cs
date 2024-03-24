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
    public bool IsActive { get; set; } = true;
    public DateTime DateOfBirth { get; set; }
    public List<Post> Posts { get; set; }
    public List<Vote> Votes { get; set; }
    public List<Complain> Complains { get; set; }
    public Gender Gender { get; set; }
    public Relationship Relationship { get; set; }
    public int? SpendingPerMonth { get; set; }
    public string? Bio { get; set; }
    public int? CountryId { get; set; }
    public Country Country { get; set; }
    public int? AvatarId { get; set; }
    public Media Avatar { get; set; }
    public List<UserCode> UserCodes { get; set; }
    public List<SavedPost> SavedPosts { get; set; }
    public List<UserJob> Jobs { get; set; }
    public List<UserHobby> Hobbies { get; set; }
    public List<UserEducation> Educations { get; set; }
}
