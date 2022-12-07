using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.User;
using Tempus.Core.Repositories;

namespace Tempus.Core.Commands.Users.Create;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, BaseResponse<BaseUser>>
{
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<BaseUser>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var entity = new User(Guid.NewGuid(), request.UserName, request.Email);
            var user = await _userRepository.Add(entity);

            var result = BaseResponse<BaseUser>.Ok(new BaseUser
            (
                user.Id,
                user.UserName,
                user.Email
            ));

            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<BaseUser>.BadRequest(exception.Message);
            return result;
        }
    }
}