﻿namespace Tempus.Core.Entities;

public class Registration : BaseEntity
{
    public Registration(Guid id, string title, string content, DateTime createdAt, DateTime lastUpdatedAt,
        Guid categoryId)
    {
        Id = id;
        Title = title;
        Content = content;
        CreatedAt = createdAt;
        LastUpdatedAt = lastUpdatedAt;
        CategoryId = categoryId;
    }

    public Registration() { }

    public string Title { get; set; }
    public string Content { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public Guid UserId { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}