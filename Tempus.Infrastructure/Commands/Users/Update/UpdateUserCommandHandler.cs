using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Commands.Users.Update;

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

            if(user == null)
            {
                return BaseResponse<BaseUser>.NotFound($"User with id {request.Id} not .");
            }

            await UpdateUser(request, user);

            var baseUser = GenericMapper<User, BaseUser>.Map(user);
            var result = BaseResponse<BaseUser>.Ok(baseUser);

            return result;
        }
        catch(Exception exception)
        {
            return BaseResponse<BaseUser>.BadRequest(new List<string> {exception.Message});
        }
    }

    private async Task UpdateUser(UpdateUserCommand request, User user)
    {
        user = new User
        {
            Id = user.Id,
            Username = request.UserName,
            Email = request.Email
        };

        await _userRepository.Update(user);
        await _userRepository.SaveChanges();
    }
}