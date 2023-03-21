using Tempus.Core.Entities;

namespace Tempus.Core.Models.Registrations;

public class BaseRegistration : BaseEntity
{
    public string Content { get; set; }
    public string Description { get; set; }
    public string CategoryColor { get; set; }

}