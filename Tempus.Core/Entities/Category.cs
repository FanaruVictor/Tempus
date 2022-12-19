#nullable enable
namespace Tempus.Core.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = "";
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public string? Color { get; set; }
    public List<Registration>? Registrations { get; set; }
    public User? User { get; set; }
    public Guid UserId { get; set; }

    public Category(Guid id, string name, DateTime createdAt, DateTime lastUpdatedAt, string color, Guid userId)
    {
        Id = id;
        Name = name;
        CreatedAt = createdAt;
        LastUpdatedAt = lastUpdatedAt;
        Color = color;
        UserId = userId;
    }

    public Category()
    {
    }
}