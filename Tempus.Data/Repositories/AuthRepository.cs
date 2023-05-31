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


	public async Task<User> Login(string email, string externalId)
	{
		email = email.ToLower();
		var user = await _context.Users.FirstOrDefaultAsync(x =>
			x.Email.ToLower() == email && x.ExternalId == externalId);

		return user;
	}

	public async Task<bool> IsEmailAlreadyRegistered(string email)
	{
		return await _context.Users.AnyAsync(x => x.Email.ToLower() == email);
	}

	public async Task<bool> IsExternalIdAlreadyRegistered(string externalId)
	{
		return await _context.Users.AnyAsync(x => x.ExternalId == externalId);
	}

	public async Task<int> SaveChanges()
	{
		return await _context.SaveChangesAsync();
	}
}