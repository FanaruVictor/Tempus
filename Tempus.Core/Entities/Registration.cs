namespace Tempus.Core.Entities;

public class Registration : BaseEntity
{
    public Registration()
    {
    }

    public Registration(
        Guid id,
        string? title,
        string? content,
        DateTime createdAt,
        DateTime lastUpdatedAt,
        Guid categoryId,
        Category category = null) : base(id)
    {
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        LastUpdatedAt = lastUpdatedAt;
        CategoryId = categoryId;
        Category = category;
    }

    public string? Title { get; }
    public string? Content { get; }
    public DateTime CreatedAt { get; }
    public DateTime LastUpdatedAt { get; }
    public Guid CategoryId { get; }
    public Category Category { get; }
}