using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Tempus.Core.Entities;

namespace Tempus.Core.Models.User;

public interface IUserInfo
{
    public string Id { get; }
    public string Username { get; }
}

public class UserInfo : IUserInfo
{
    public UserInfo(ClaimsPrincipal user)
    {
        if(user != null)
        {
            Id = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            Username = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        }
    }
    
    public string Id { get; }
    public string Username { get; }
}

public class AspUserInfo : UserInfo
{
    public AspUserInfo(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor?.HttpContext?.User) { }
}