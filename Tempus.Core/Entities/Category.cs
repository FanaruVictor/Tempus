#nullable enable
namespace Tempus.Core.Entities;

public class Category : BaseEntity
{
    public Category()
    {
    }

    public Category(
        Guid id,
        string name,
        DateTime createdAt,
        DateTime lastUpdatedAt,
        string color,
        Guid userId,
        List<Registration>? registrations = null,
        User? user = null
    ) : base(id)
    {
        Name = name;
        CreatedAt = createdAt;
        LastUpdatedAt = lastUpdatedAt;
        Color = color;
        User = user;
        UserId = userId;
        Registrations = registrations;
    }

    public string Name { get; } = "";
    public DateTime CreatedAt { get; }
    public DateTime LastUpdatedAt { get; }
    public string? Color { get; }
    public List<Registration>? Registrations { get; }
    public User? User { get; }
    public Guid UserId { get; }
}