namespace Tempus.Core.Entities;

public class User : BaseEntity
{ 
    public string Username { get; set; }
    public string Email { get; set; }
    public byte[] Password { get; set; }
    public byte[] PasswordSalt { get; set; }
    
    public List<Registration>? Registrations { get; set; }
    public List<Category>? Categories { get; set; }
    
    public User(Guid id, string username, string email)
    {
        Id = id;
        Username = username;
        Email = email;
    }

    public User()
    {
        
    }
}