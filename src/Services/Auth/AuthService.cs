﻿using IWantApp.DTOs.User;
using IWantApp.Utils;
using IWantApp.Services.Exceptions;

namespace IWantApp.Services.Auth;

public class AuthService : IAuthService
{
    private readonly CustomUserUtil _customUserUtil;
    private readonly UserDtoToEntityAdapter _adapter;
    private readonly UserRepository _userRepository;

    public AuthService(CustomUserUtil customUserUtil, UserDtoToEntityAdapter adapter, UserRepository userRepository)
    {
        _customUserUtil = customUserUtil;
        _adapter = adapter;
        _userRepository = userRepository;
    }

    public UserDTO Authenticated()
    {
        try
        {
            string username = _customUserUtil.GetLoggedUsername();
            Models.User user = _userRepository.GetByUsername(username);

            if (user == null)
            {
                throw new ResourceNotFoundException("Invalid user");
            }

            return _adapter.ToDto(user);
        }
        catch (Exception e)
        {
            throw new ResourceNotFoundException("Invalid user");
        }
    }

    public void ValidateSelfOrAdmin(int userId)
    {
        try
        {
            string username = _customUserUtil.GetLoggedUsername();
            Models.User loggedUser = _userRepository.GetByUsername(username);

            if (loggedUser == null)
            {
                throw new ResourceNotFoundException("Logged user not found");
            }

            // Verifica se o usuário logado é o mesmo que o ID fornecido ou se é um admin
            if (loggedUser.Id != userId && !loggedUser.UserRoles.Any(ur => ur.RoleId == 2))
            {
                throw new ForbiddenException("User is not authorized");
            }
        }
        catch (Exception e)
        {
            throw new Exception("Error validating user authorization", e);
        }
    }
}
