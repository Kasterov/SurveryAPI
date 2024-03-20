using Application.DTOs.Common;
using Application.DTOs.Countries;
using Application.DTOs.Educations;
using Application.DTOs.Hobbies;
using Application.DTOs.Jobs;

namespace Application.DTOs.Profiles;

public class ProfileViewDTO : BaseAuditableDTO
{
    public int? AvatarId { get; set; }
    public string AvatarLink { get; set; }
    public string Name { get; set; }
    public string Bio { get; set; }
    public CountryDTO Country { get; set; }
    public DateTimeOffset RegistrationDate { get; set; }
    public IEnumerable<EducationDTO> Educations { get; set; }
    public IEnumerable<JobDTO> Jobs { get; set; }
    public IEnumerable<HobbyDTO> Hobbies { get; set; }
}
