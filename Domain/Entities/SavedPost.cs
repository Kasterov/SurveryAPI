namespace Domain.Entities;

public class SavedPost
{
    public int UserId { get; set; }
    public User User { get; set; }

    public int PostId { get; set; }
    public Post Post { get; set; }
}
