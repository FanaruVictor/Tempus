using Tempus.Core.Entities;

namespace Tempus.Infrastructure.Models.Registrations;

public class BaseRegistration : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
}