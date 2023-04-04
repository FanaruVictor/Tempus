
using Tempus.Core.Entities;
using Tempus.Core.Entities.Group;

public class GroupPhoto : Photo
{
    public Guid GroupId { get; set; }
    public Group Group { get; set; }
}