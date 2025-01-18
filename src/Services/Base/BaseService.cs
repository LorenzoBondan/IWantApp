namespace IWantApp.Services.Base;

using IWantApp.DTOs.Base;
using IWantApp.Models.Base;
using IWantApp.Repositories.Base;
using System.Linq.Expressions;

public class BaseService<T, Dto> where T : BaseEntity where Dto : BaseDTO
{
    private readonly IRepository<T> _repository;
    private readonly BaseConverter<T, Dto> _converter;

    public BaseService(IRepository<T> repository, BaseConverter<T, Dto> converter)
    {
        _repository = repository;
        _converter = converter;
    }

    public IQueryable<Dto> FindAll()
    {
        return _repository.FindAll().Select(t => _converter.ToDto(t));
    }

    public async Task<List<Dto>> FindWithPagedSearch(string query)
    {
        var entities = await _repository.FindWithPagedSearch(query);
        return entities.Select(_converter.ToDto).ToList();
    }

    public async Task<Dto?> FindByIdAsync(int id)
    {
        var entity = await _repository.FindById(id);
        return entity == null ? null : _converter.ToDto(entity);
    }

    public async Task<Dto> CreateAsync(Dto dto)
    {
        return _converter.ToDto(await _repository.Create(_converter.ToEntity(dto)));
    }

    public async Task<Dto?> UpdateAsync(Dto dto)
    {
        var updatedEntity = await _repository.Update(_converter.ToEntity(dto));
        return updatedEntity == null ? null : _converter.ToDto(updatedEntity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.Delete(id);
    }

    public async Task<bool> ExistsByIdAsync(int id)
    {
        return await _repository.ExistsById(id);
    }

    public async Task<int> GetCountAsync(Expression<Func<T, bool>> predicate)
    {
        return await _repository.GetCount(predicate);
    }
}

