namespace Tempus.Core.Entities.Group;

public class GroupCategory
{
    public Guid GroupId { get; set; }  
    public Group Group { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}