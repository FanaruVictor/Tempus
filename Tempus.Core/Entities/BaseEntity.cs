namespace Tempus.Core.Entities;

public class BaseEntity
{
    public BaseEntity()
    {
    }

    public BaseEntity(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }
}