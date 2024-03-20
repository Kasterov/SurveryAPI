using Application.DTOs.Common;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.Profiles;

public class UserProfileDTO : BaseDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public Relationship Relationship { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int? SpendingPerMonth { get; set; }
    public string Bio { get; set; }
    public int? CountryId { get; set; }
    public string AvatarLink { get; set; }
    public int AvatarId { get; set; }
    public Country Country { get; set; }
    public List<UserJob> Jobs { get; set; }
    public List<UserHobby> Hobbies { get; set; }
    public List<UserEducation> Educations { get; set; }
}
