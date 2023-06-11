using Tempus.Core.Entities;

namespace Tempus.Core.Models.User;

public class UserEmail : BaseEntity
{
    public string Email { get; set; }
    public string PhotoUrl { get; set; }
}