﻿using Messenger.Auth.Core.Business;
using Messenger.Auth.Dto;

namespace Messenger.Auth.Services;

public interface IUserService : IService<UserDto>
{
    Task<bool> CheckPasswordCorrect(string email, string password, CancellationToken cancellationToken);
    Task<UserDto> GetByEmail(string email, CancellationToken cancellationToken);
    Task<UserDto> GetUserByRefreshToken(Guid refreshToken, CancellationToken cancellationToken);
    Task<int> RegisterUser(UserDto dto, CancellationToken cancellationToken);
}
