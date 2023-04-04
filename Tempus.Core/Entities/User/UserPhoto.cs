namespace Tempus.Core.Entities.User;

public class UserPhoto : Photo
{ 
    public Guid UserId { get; set; }
    public User User { get; set; }
}