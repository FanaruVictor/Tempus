using Tempus.Core.Entities;

namespace Tempus.Core.Commons;

public class UserDetails : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public PhotoDetails Photo { get; set; }
    public bool IsDarkTheme { get; set; }
    public string ExternalId { get; set; }
}