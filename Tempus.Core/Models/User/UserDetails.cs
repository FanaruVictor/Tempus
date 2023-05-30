using Tempus.Core.Entities;
using Tempus.Core.Models.Photo;

namespace Tempus.Core.Models.User;

public class UserDetails : BaseEntity
{
    public string Email { get; set; }
    public PhotoDetails Photo { get; set; }
    public bool IsDarkTheme { get; set; }
    public string ExternalId { get; set; }
}