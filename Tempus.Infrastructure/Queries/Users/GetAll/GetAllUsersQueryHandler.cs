﻿using MediatR;
using Tempus.Core.Commons;
using Tempus.Core.Entities;
using Tempus.Core.IRepositories;
using Tempus.Infrastructure.Commons;
using Tempus.Infrastructure.Models.User;

namespace Tempus.Infrastructure.Queries.Users.GetAll;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, BaseResponse<List<UserDetails>>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<BaseResponse<List<UserDetails>>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            cancellationToken.ThrowIfCancellationRequested();

            var users = await _userRepository.GetAll();

            var result =
                BaseResponse<List<UserDetails>>.Ok(users.Select(GenericMapper<User, UserDetails>.Map).ToList());
            return result;
        }
        catch(Exception exception)
        {
            var result = BaseResponse<List<UserDetails>>.BadRequest(new List<string> {exception.Message});
            return result;
        }
    }
}