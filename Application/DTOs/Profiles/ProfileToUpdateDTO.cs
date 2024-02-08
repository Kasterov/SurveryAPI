using Application.DTOs.Common;
using Application.DTOs.Countries;
using Application.DTOs.Educations;
using Application.DTOs.Hobbies;
using Application.DTOs.Jobs;
using Application.DTOs.ProfileEducations;
using Application.DTOs.ProfileHobbies;
using Domain.Enums;

namespace Application.DTOs.Profiles;

public class ProfileToUpdateDTO : BaseDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Gender Gender { get; set; }
    public Relationship Relationship { get; set; }
    public SallaryGrade Sallary { get; set; }
    public string? Bio { get; set; }
    public int? FileEntityId { get; set; }
    public string FileEntityLink { get; set; }
    public UploadCountryDTO Country { get; set; }
    public List<UploadJobDTO> Jobs { get; set; }
    public List<UploadHobbyDTO> Hobbies { get; set; }
    public List<UploadEducationDTO> Educations { get; set; }
}