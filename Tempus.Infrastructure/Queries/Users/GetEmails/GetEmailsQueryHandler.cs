using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.IRepositories;
using Tempus.Core.Models.User;

namespace Tempus.Infrastructure.Queries.Users.GetEmails;

public class GetEmailsQueryHandler : IRequestHandler<GetEmailsQuery,BaseResponse<List<UserEmail>>>
{
    private readonly IUserRepository _userRepository;

    public GetEmailsQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task<BaseResponse<List<UserEmail>>> Handle(GetEmailsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var userEmails = (await _userRepository.GetUsersEmails()).Where(x => x.Id != request.UserId).ToList();

            return BaseResponse<List<UserEmail>>.Ok(userEmails);
        }
        catch(Exception e)
        {
            return BaseResponse<List<UserEmail>>.BadRequest(new List<string>
            {
                e.Message
            });
        }
    }
}