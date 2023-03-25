using Microsoft.AspNetCore.Http;
using Tempus.Core.Commons;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Users.Update;

public class UpdateUserCommand : BaseRequest<BaseResponse<UserDetails>>
{
    public string UserName { get; init; }
    public string Email { get; init; }
    
    public string? PhoneNumber { get; init; }

    public bool IsPhotoChanged { get; set; }
    public IFormFile? NewPhoto { get; set; }
}