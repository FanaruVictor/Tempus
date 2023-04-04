﻿namespace Tempus.Core.Entities;

public class Registration : BaseEntity
{
  public string Description { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime LastUpdatedAt { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
}