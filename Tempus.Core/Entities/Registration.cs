namespace Tempus.Core.Entities;

public class Registration : BaseEntity
{
	public string Title { get; set; }
	public string Content { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime LastUpdatedAt { get; set; }
	public Guid UserId { get; set; }
	public User User { get; set; }
	public Guid CategoryId { get; set; }
	public Category Category { get; set; }
}