using Tempus.Core.Entities;

namespace Tempus.Infrastructure.Models.User;

public class BaseUser : BaseEntity
{
    public string UserName { get; set; }
    public string Email { get; set; }
}