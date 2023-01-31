namespace Tempus.Core.Entities;

public class ProfilePhoto : BaseEntity
{
    public string PublicId { get; set; }
    public string Url { get; set; }
    public User User { get; set; }
    public Guid UserId { get; set; }
}