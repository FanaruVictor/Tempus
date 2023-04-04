
namespace Tempus.Core.Entities.Group;

public class Group : BaseEntity
{
    public string Name { get; set; }
    public Guid OwnerId { get; set; }
    public DateTime CreatedAt { get; set; }
    public List<GroupUser> GroupUsers { get; set; }
    public List<GroupCategory> GroupCategories { get; set; }
    public GroupPhoto GroupPhoto { get; set; }
}