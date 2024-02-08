using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class Profile : BaseAuditableEntity
{
    public Gender Gender { get; set; }
    public Relationship Relationship { get; set; }
    public SallaryGrade Sallary { get; set; }
    public string? Bio { get; set; }
    public int? CountryId { get; set; }
    public Country Country { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int? FileEntityId { get; set; }
    public FileEntity FileEntity { get; set; }
    public List<ProfileJob> Jobs { get; set; }
    public List<ProfileHobby> Hobbies { get; set; }
    public List<ProfileEducation> Educations { get; set; }
}
