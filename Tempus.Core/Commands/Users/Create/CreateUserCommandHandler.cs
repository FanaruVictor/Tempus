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
            
            var entity = new User
            {
                Id = Guid.NewGuid(),
                UserName = request.UserName,
                Email = request.Email 
                
            };
            
            var user = await _userRepository.Add(entity);

            var baseUser = GenericMapper<User, BaseUser>.Map(user);
            var result = BaseResponse<BaseUser>.Ok(baseUser);

            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<BaseUser>.BadRequest(new List<string>{exception.Message});
            return result;
        }
    }
}