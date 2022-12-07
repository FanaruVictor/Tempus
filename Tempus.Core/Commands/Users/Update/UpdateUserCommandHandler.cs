using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.User;
using Tempus.Core.Repositories;

namespace Tempus.Core.Commands.Users.Update;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse<BaseUser>>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<BaseUser>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetById(request.Id);

            if (user == null) return BaseResponse<BaseUser>.BadRequest($"User with id {request.Id} not found");

            user = new User(user.Id, request.UserName, request.Email);

            await _userRepository.Update(user);

            var result = BaseResponse<BaseUser>.Ok(new BaseUser(user.Id, user.UserName, user.Email));
            return result;
        }
        catch (Exception exception)
        {
            return BaseResponse<BaseUser>.BadRequest(exception.Message);
        }
    }
}