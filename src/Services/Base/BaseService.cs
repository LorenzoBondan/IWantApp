namespace IWantApp.Services.Base;

using IWantApp.DTOs.Base;
using IWantApp.Models.Base;
using IWantApp.Repositories.Base;
using IWantApp.Services.Exceptions;
using IWantApp.Utils;
using System.Linq.Expressions;

public class BaseService<T, Dto> : IBaseService<T, Dto> where T : BaseEntity where Dto : BaseDTO
{
    protected IRepository<T> _repository;
    protected BaseConverter<T, Dto> _converter;

    public BaseService(IRepository<T> repository, BaseConverter<T, Dto> converter)
    {
        _repository = repository;
        _converter = converter;
    }

    public IQueryable<Dto> FindAll(params Expression<Func<T, object>>[] includeProperties)
    {
        var entities = _repository.FindAll(includeProperties);
        return entities.Select(t => _converter.ToDto(t));
    }

    public async Task<List<Dto>> FindWithPagedSearch(string query)
    {
        var entities = await _repository.FindWithPagedSearch(query);
        return entities.Select(_converter.ToDto).ToList();
    }

    public async Task<PageableResponse<Dto>> GetAllPaged(
    int page,
    int size,
    string? sortedBy = null,
    Expression<Func<T, bool>>? filter = null,
    params Expression<Func<T, object>>[] includeProperties)
    {
        // Chama o método genérico de paginação do repositório
        var pagedResult = await _repository.GetAllPaged(page, size, sortedBy, filter, includeProperties);

        // Converte a lista de entidades para DTOs
        var content = pagedResult.Content.Select(_converter.ToDto).ToList();

        // Retorna o resultado paginado
        return new PageableResponse<Dto>
        {
            NumberOfElements = pagedResult.NumberOfElements,
            Page = pagedResult.Page,
            TotalPages = pagedResult.TotalPages,
            Size = pagedResult.Size,
            First = pagedResult.First,
            Last = pagedResult.Last,
            SortedBy = pagedResult.SortedBy,
            Content = content
        };
    }

    public async Task<Dto?> FindById(int id, Expression<Func<T, object>> includeProperty)
    {
        var entity = await _repository.FindById(id, includeProperty);
        if (entity == null)
        {
            throw new ResourceNotFoundException($"Recurso com ID {id} não encontrado.");
        }
        return _converter.ToDto(entity);
    }

    public async Task<Dto> Create(Dto dto)
    {
        return _converter.ToDto(await _repository.Create(_converter.ToEntity(dto)));
    }

    public async Task<Dto?> Update(Dto dto)
    {
        if (!await ExistsById(dto.Id))
        {
            throw new ResourceNotFoundException($"Recurso com ID {dto.Id} não encontrado para atualização.");
        }

        var updatedEntity = await _repository.Update(_converter.ToEntity(dto));
        return updatedEntity == null ? null : _converter.ToDto(updatedEntity);
    }

    public async Task Delete(int id)
    {
        if (!await ExistsById(id))
        {
            throw new ResourceNotFoundException($"Recurso com ID {id} não encontrado para exclusão.");
        }

        await _repository.Delete(id);
    }

    public async Task<bool> ExistsById(int id)
    {
        return await _repository.ExistsById(id);
    }

    public async Task<int> GetCount(Expression<Func<T, bool>> predicate)
    {
        return await _repository.GetCount(predicate);
    }
}

