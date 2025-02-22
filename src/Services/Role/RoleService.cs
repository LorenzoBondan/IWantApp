﻿using IWantApp.DTOs.Role;
using IWantApp.Services.Exceptions;
using IWantApp.Utils;

namespace IWantApp.Services.Role;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _repository;
    private readonly RoleDtoToEntityAdapter _adapter;

    public RoleService(IRoleRepository roleRepository, RoleDtoToEntityAdapter roleDtoToEntityAdapter)
    {
        _repository = roleRepository;
        _adapter = roleDtoToEntityAdapter;
    }

    public async Task<List<RoleDTO>> GetAll()
    {
        var list = await _repository.GetAll();
        return list.Select(r => _adapter.ToDto(r)).ToList();
    }

    public async Task<PageableResponse<RoleDTO>> GetAllPaged(int page, int size, string? sortedBy = null)
    {
        var pagedResponse = await _repository.GetAllPaged(page, size, sortedBy);

        var contentDto = pagedResponse.Content
            .Select(r => _adapter.ToDto(r))
            .ToList();

        return new PageableResponse<RoleDTO>
        {
            NumberOfElements = pagedResponse.NumberOfElements,
            Page = pagedResponse.Page,
            TotalPages = pagedResponse.TotalPages,
            Size = pagedResponse.Size,
            First = pagedResponse.First,
            Last = pagedResponse.Last,
            SortedBy = pagedResponse.SortedBy,
            Content = contentDto
        };
    }

    public async Task<RoleDTO> GetById(int id)
    {
        var obj = await _repository.GetById(id);
        if (obj == null)
        {
            throw new ResourceNotFoundException("Role não encontrada com id: " + id);
        }
        return _adapter.ToDto(obj);
    }

    public async Task Create(RoleDTO dto)
    {
        var entity = _adapter.ToEntity(dto);
        await _repository.Create(entity);
    }

    public async Task Update(RoleDTO dto)
    {
        var entity = _adapter.ToEntity(dto);
        await _repository.Update(entity);
    }

    public async Task Delete(int id)
    {
        await _repository.Delete(id);
    }
}
