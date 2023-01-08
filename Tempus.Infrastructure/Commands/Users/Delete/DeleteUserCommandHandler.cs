using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Repositories;

namespace Tempus.Infrastructure.Commands.Users.Delete;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, BaseResponse<Guid>>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<Guid>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            var deletedUserId = await _userRepository.Delete(request.Id);

            BaseResponse<Guid> result;
            if (deletedUserId == Guid.Empty)
            {
                result = BaseResponse<Guid>.NotFound($"User with Id: {request.Id} not found");
                return result;
            }
            
            result = BaseResponse<Guid>.Ok(deletedUserId);
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<Guid>.BadRequest(new List<string>{exception.Message});
            return result;
        }
    }
}