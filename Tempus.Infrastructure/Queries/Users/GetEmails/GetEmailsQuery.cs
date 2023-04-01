using Tempus.Core.Commons;
using Tempus.Core.Models.User;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Queries.Users.GetEmails;

public class GetEmailsQuery : BaseRequest<BaseResponse<List<UserEmail>>>
{
}