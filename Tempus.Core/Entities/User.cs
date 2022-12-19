namespace Tempus.Core.Entities;

public class User : BaseEntity
{ 
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<Registration>? Registrations { get; set; }
    public List<Category>? Categories { get; set; }
    
    public User(Guid id, string userName, string email)
    {
        Id = id;
        UserName = userName;
        Email = email;
    }

    public User()
    {
        
    }
}