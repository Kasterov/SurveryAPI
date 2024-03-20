using Application.DTOs.FileEntities;
using Application.DTOs.ProfileEducations;
using Application.DTOs.ProfileHobbies;
using Application.DTOs.ProfileJobs;
using Domain.Entities;
using Domain.Enums;

namespace Application.DTOs.Profiles;

public class UserProfileUpdateDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get;set;}
    public Gender Gender { get; set; }
    public Relationship Relationship { get; set; }
    public int? SpendingPerMonth { get; set; }
    public string? Bio { get; set; }
    public int? CountryId { get; set; }
    public UploadFileDTO? Avatar { get; set; }
    public List<CreateProfileJobDTO> Jobs { get; set; }
    public List<CreateProfileHobbyDTO> Hobbies { get; set; }
    public List<CreateProfileEducationDTO> Educations { get; set; }
}
