using IWantApp.DTOs.Base;
using IWantApp.Models.Base;
using IWantApp.Utils;
using System.Linq.Expressions;

namespace IWantApp.Services.Base;

public interface IBaseService<T, Dto> where T : BaseEntity where Dto : BaseDTO
{
    IQueryable<Dto> FindAll(params Expression<Func<T, object>>[] includeProperties);
    Task<List<Dto>> FindWithPagedSearch(string query);
    Task<PageableResponse<Dto>> GetAllPaged(
    int page,
    int size,
    string? sortedBy = null,
    Expression<Func<T, bool>>? filter = null,
    params Expression<Func<T, object>>[] includeProperties);
    Task<Dto?> FindById(int id, Expression<Func<T, object>> includeProperty);
    Task<Dto> Create(Dto dto);
    Task<Dto?> Update(Dto dto);
    Task Delete(int id);
    Task<bool> ExistsById(int id);
    Task<int> GetCount(Expression<Func<T, bool>> predicate);
}
