namespace Tempus.Core.Entities.Group;

public class GroupUser
{
    public Guid GroupId { get; set; }
    public Guid UserId { get; set; }
    public User.User User { get; set; }
    public Group Group { get; set; }
}