namespace Tempus.Core.Models.Group;

public class GroupOverview
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public List<string> UserPhotos { get; set; }
    public DateTime CreatedAt { get; set; }
}