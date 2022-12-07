namespace Tempus.Core.Entities;

public class User : BaseEntity
{
    public User()
    {
    }

    public User(
        Guid id,
        string userName,
        string email,
        List<Registration>? registrations = null) : base(id)
    {
        UserName = userName;
        Email = email;
        Registrations = registrations;
    }

    public string UserName { get; }
    public string Email { get; }
    public List<Registration>? Registrations { get; }
    public List<Category>? Categories { get; }
}