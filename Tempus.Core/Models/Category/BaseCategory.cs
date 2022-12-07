using Tempus.Core.Entities;

namespace Tempus.Core.Models.Category;

public class BaseCategory : BaseEntity
{
    public BaseCategory(
        Guid id,
        string name,
        DateTime lastUpdatedAt,
        string color,
        Guid userId
    ) : base(id)
    {
        Name = name;
        LastUpdatedAt = lastUpdatedAt;
        Color = color;
        UserId = userId;
    }

    public string Name { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public string? Color { get; set; }
    public Guid UserId { get; set; }
}