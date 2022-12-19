using Tempus.Core.Entities;

namespace Tempus.Core.Models.Category;

public class BaseCategory : BaseEntity
{
    public string Name { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public string? Color { get; set; }
    public Guid UserId { get; set; }
}