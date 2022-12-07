using Tempus.Core.Entities;

namespace Tempus.Core.Models.User;

public class BaseUser : BaseEntity
{
    public BaseUser(Guid id, string userName, string email) : base(id)
    {
        UserName = userName;
        Email = email;
    }

    public string UserName { get; }
    public string Email { get; }
}