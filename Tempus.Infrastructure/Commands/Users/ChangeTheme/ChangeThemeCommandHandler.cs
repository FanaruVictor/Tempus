using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Commands.Users.ChangeTheme;

public class ChangeThemeCommandHandler : IRequestHandler<ChangeThemeCommand, BaseResponse<UserDetails>>
{
    private readonly IUserRepository _userRepository;

    public ChangeThemeCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<UserDetails>> Handle(ChangeThemeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var user = await _userRepository.GetById(request.UserId);

            BaseResponse<UserDetails> result;

            if(user == null)
            {
                result = BaseResponse<UserDetails>.NotFound("User not found");

                return result;
            }

            user.IsDarkTheme = request.IsDarkTheme;

            await _userRepository.Update(user);
            await _userRepository.SaveChanges();

            var userDetails = GenericMapper<User, UserDetails>.Map(user);

            result = BaseResponse<UserDetails>.Ok(userDetails);

            return result;
        }
        catch(Exception exception)
        {
            return BaseResponse<UserDetails>.BadRequest(new List<string>
            {
                exception.Message
            });
        }
    }
}