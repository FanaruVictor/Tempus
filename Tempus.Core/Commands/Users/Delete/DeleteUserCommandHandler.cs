using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Repositories;

namespace Tempus.Core.Commands.Users.Delete;

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

            var result = BaseResponse<Guid>.Ok(deletedUserId);
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<Guid>.BadRequest(exception.Message);
            return result;
        }
    }
}