using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
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

	public async Task Register(User user)
	{
		await _context.Users.AddAsync(user);
	}


	public async Task<User> Login(string email)
	{
		email = email.ToLower();
		var user = await _context.Users.Include(x => x.UserPhoto).FirstOrDefaultAsync(x =>
			x.Email.ToLower() == email);

		return user;
	}

	public async Task<bool> IsEmailAlreadyRegistered(string email)
	{
		email = email.ToLower();
		return await _context.Users.AnyAsync(x => x.Email.ToLower() == email);
	}

	public async Task<int> SaveChanges()
	{
		return await _context.SaveChangesAsync();
	}
}