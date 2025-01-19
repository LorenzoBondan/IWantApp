using IWantApp.DTOs.User;
using IWantApp.Models;
using IWantApp.Services.Auth;
using IWantApp.Services.Exceptions;
using IWantApp.Services.User;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly UserDtoToEntityAdapter _adapter;
    private readonly IAuthService _authService;
    private readonly UserManager<User> _userManager;

    public UserService(IUserRepository userRepository, UserDtoToEntityAdapter userDTOToEntityAdapter, IAuthService authService, UserManager<User> userManager)
    {
        _repository = userRepository;
        _adapter = userDTOToEntityAdapter;
        _authService = authService;
        _userManager = userManager;
    }

    public async Task<List<UserDTO>> GetAll()
    {
        var list = await _repository.GetAll();
        return list.Select(r => _adapter.ToDto(r)).ToList();
    }

    public async Task<UserDTO> GetById(int id)
    {
        _authService.ValidateSelfOrAdmin(id);
        var obj = await _repository.GetById(id);
        if (obj == null)
        {
            throw new ResourceNotFoundException("User não encontrado com id: " + id);
        }
        return _adapter.ToDto(obj);
    }

    public UserDTO GetByUsername(string username)
    {
        var obj = _repository.GetByUsername(username);
        if (obj == null)
        {
            throw new ResourceNotFoundException("User não encontrado com username: " + username);
        }
        return _adapter.ToDto(obj);
    }

    public async Task Create(UserPersistenceDTO dto)
    {
        var entity = _adapter.ToEntity(dto);
        entity.PasswordHash = _userManager.PasswordHasher.HashPassword(entity, dto.Password);
        await _repository.Create(entity);
    }

    public async Task Update(UserPersistenceDTO dto)
    {
        _authService.ValidateSelfOrAdmin(dto.Id);
        var entity = _adapter.ToEntity(dto);
        entity.PasswordHash = _userManager.PasswordHasher.HashPassword(entity, dto.Password);
        await _repository.Update(entity);
    }

    public async Task Delete(int id)
    {
        _authService.ValidateSelfOrAdmin(id);
        await _repository.Delete(id);
    }
}
