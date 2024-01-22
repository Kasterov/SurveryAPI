namespace Domain.Entities;

public class ProfileJob
{
    public int ProfileId { get; set; }
    public Profile Profile { get; set; }

    public int JobId { get; set; }
    public Job Job { get; set; }
}
