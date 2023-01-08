using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Tempus.Core.Entities;
using Tempus.Core.Repositories;
using Tempus.Data.Context;

namespace Tempus.Data.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly TempusDbContext _context;

    public AuthRepository(TempusDbContext context)
    {
        _context = context;
    }
    
    public async Task<User> Register(User user, string password)
    {
        CreatePasswordHash(password, out var passwordHash, out var passwordSalt);

        user.Password = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return user;
    }

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }


    public async Task<User> Login(string username, string password)
    {
        username = username.ToLower();
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

        if (user == null)
        {
            return null;
        }

        if (!VerifyPasswordHash(password, user.Password, user.PasswordSalt))
        {
            return null;
        }

        return user;
    }

    private bool VerifyPasswordHash(string password, byte[] userPassword, byte[] userPasswordSalt)
    {
        using var hmac = new HMACSHA512(userPasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

        return !computedHash.Where((t, i) => t != userPassword[i]).Any();
    }

    public async Task<bool> UserExists(string username)
    {
        return await _context.Users.AnyAsync(x => x.Username == username);
    }
}