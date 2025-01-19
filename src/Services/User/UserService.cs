using IWantApp.DTOs.User;
using IWantApp.Services.Auth;
using IWantApp.Services.Exceptions;

namespace IWantApp.Services.User;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly UserDtoToEntityAdapter _adapter;
    private readonly IAuthService _authService;

    public UserService(IUserRepository userRepository, UserDtoToEntityAdapter userDTOToEntityAdapter, IAuthService authService)
    {
        _repository = userRepository;
        _adapter = userDTOToEntityAdapter;
        _authService = authService;
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

    public async Task<UserDTO> GetByUsername(string username)
    {
        var obj = await _repository.GetByUsername(username);
        if (obj == null)
        {
            throw new ResourceNotFoundException("User não encontrado com username: " + username);
        }
        return _adapter.ToDto(obj);
    }

    public async Task Create(UserDTO dto)
    {
        var entity = _adapter.ToEntity(dto);
        await _repository.Create(entity);
    }

    public async Task Update(UserDTO dto)
    {
        _authService.ValidateSelfOrAdmin(dto.Id);
        var entity = _adapter.ToEntity(dto);
        await _repository.Update(entity);
    }

    public async Task Delete(int id)
    {
        _authService.ValidateSelfOrAdmin(id);
        await _repository.Delete(id);
    }
}
