using Application.DTOs.Common;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.Profiles;

public class ProfileDTO : BaseDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Gender Gender { get; set; }
    public Relationship Relationship { get; set; }
    public SallaryGrade Sallary { get; set; }
    public string Bio { get; set; }
    public int? CountryId { get; set; }
    public Country Country { get; set; }
    public List<ProfileJob> Jobs { get; set; }
    public List<ProfileHobby> Hobbies { get; set; }
    public List<ProfileEducation> Educations { get; set; }
}
