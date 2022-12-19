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

            if (user == null) return BaseResponse<BaseUser>.NotFound($"User with id {request.Id} not .");

            user = new User{
                Id = user.Id, 
                UserName = request.UserName, 
                Email = request.Email
            };

            await _userRepository.Update(user);

            var baseUser = GenericMapper<User, BaseUser>.Map(user);
            var result = BaseResponse<BaseUser>.Ok(baseUser);
            
            return result;
        }
        catch (Exception exception)
        {
            return BaseResponse<BaseUser>.BadRequest(new List<string>{exception.Message});
        }
    }
}