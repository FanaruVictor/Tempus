namespace Tempus.Core.Entities;

public class User : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<Registration>? Registrations { get; set; }
}