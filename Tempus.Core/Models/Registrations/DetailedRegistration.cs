using Tempus.Core.Entities;

namespace Tempus.Core.Models.Registrations;

public class DetailedRegistration : BaseRegistration
{
    public DateTime CreatedAt { get; set; }
    public string CategoryColor { get; set; }
}