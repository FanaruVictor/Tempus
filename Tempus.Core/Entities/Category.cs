namespace Tempus.Core.Entities;

public class Category : BaseEntity
{
	public string Name { get; set; }
	public DateTime CreatedAt { get; set; }
	public string Color { get; set; }
	public List<Registration> Registrations { get; set; }
}