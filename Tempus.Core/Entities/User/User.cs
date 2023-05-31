using Tempus.Core.Entities.Group;

namespace Tempus.Core.Entities.User;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public bool IsDarkTheme { get; set; }
    public string? ExternalId { get; set; }
    public List<GroupUser> GroupUsers { get; set; }
    public UserPhoto UserPhoto { get; set; }

    public List<UserCategory> UserCategories { get; set; }
}