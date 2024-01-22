namespace Domain.Entities;

public class ProfileEducation
{
    public int ProfileId { get; set; }
    public Profile Profile { get; set; }

    public int EducationId { get; set; }
    public Education Education { get; set; }
}
