namespace Domain.Entities;

public class ProfileHobby
{
    public int ProfileId { get; set; }
    public Profile Profile { get; set; }

    public int HobbyId { get; set; }
    public Hobby Hobby { get; set; }
}
