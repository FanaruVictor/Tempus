using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.Models.User;
using Tempus.Core.Repositories;
using Tempus.Infrastructure.Commons;

namespace Tempus.Infrastructure.Commands.Auth.Register;

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse<BaseUser>>
{
    private readonly IAuthRepository _authRepository;

    public RegisterUserCommandHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }

    public async Task<BaseResponse<BaseUser>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            BaseResponse<BaseUser> result;
            
            var username = request.UserName.ToLower();

            if ( await _authRepository.UserExists(username))
            {
                result = BaseResponse<BaseUser>.BadRequest(new List<string>
                {
                    "Username already exists"
                });

                return result;
            }
            
            var entity = new User
            {
                Id = Guid.NewGuid(),
                Username = request.UserName,
                Email = request.Email
            };
             
            var user = await _authRepository.Register(entity, request.Password);

            var baseUser = GenericMapper<User, BaseUser>.Map(user);
            result = BaseResponse<BaseUser>.Ok(baseUser);
            
            return result;
        }
        catch (Exception exception)
        {
            var result = BaseResponse<BaseUser>.BadRequest(new List<string>{exception.Message});
            return result;
        }
    }
}