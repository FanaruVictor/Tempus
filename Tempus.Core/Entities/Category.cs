#nullable enable

using Tempus.Core.Entities.Group;
using Tempus.Core.Entities.User;

namespace Tempus.Core.Entities;

public class Category : BaseEntity
{ 
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public string? Color { get; set; }
    public List<Registration> Registrations { get; set; }
    public List<UserCategory> UserCategories { get; set; }
    public List<GroupCategory> GroupCategories { get; set; }
}