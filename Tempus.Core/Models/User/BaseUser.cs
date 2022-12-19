using Tempus.Core.Entities;

namespace Tempus.Core.Models.User;

public class BaseUser : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
}