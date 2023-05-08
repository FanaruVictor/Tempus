using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.Entities.User;
using Tempus.Core.IRepositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly TempusDbContext _context;

    public AuthRepository(TempusDbContext context)
    {
        _context = context;
    }

    public async Task Register(User user, string password)
    {
        if (string.IsNullOrEmpty(user.ExternalId))
        {
            CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

            user.Password = passwordHash;
            user.PasswordSalt = passwordSalt;

        }
        
        await _context.Users.AddAsync(user);
    }


    public async Task<User> Login(string email, string password)
    {
        email = email.ToLower();
        var user = await _context.Users.FirstOrDefaultAsync(x =>
            x.Email.ToLower() == email && string.IsNullOrEmpty(x.ExternalId));
        
        if(!VerifyPasswordHash(password, user.Password, user.PasswordSalt))
        {
            throw new Exception("Wrong username or password");
        }

        return user;
    }

    public async Task<bool> IsEmailAlreadyRegistered(string email)
    {
        return await _context.Users.AnyAsync(x => x.Email.ToLower() == email && string.IsNullOrEmpty(x.ExternalId));
    }
    
    public async Task<bool> IsUsernameAlreadyRegistered(string username)
    {
        return await _context.Users.AnyAsync(x => x.Username.ToLower() == username && string.IsNullOrEmpty(x.ExternalId));
    }
    
    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] userPassword, byte[] userPasswordSalt)
    {
        using var hmac = new HMACSHA512(userPasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return!computedHash.Where((t, i) => t != userPassword[i]).Any();
    }
}