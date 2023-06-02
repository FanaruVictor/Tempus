using Microsoft.AspNetCore.Http;
using Tempus.Core.Commons;
using Tempus.Core.Models.Group;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Groups.Update;

public class UpdateGroupCommand : BaseRequest<BaseResponse<GroupOverview>>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Members { get; set; }
    public IFormFile? Image { get; set; }
    public bool IsCurrentImageChanged { get; set; }
}