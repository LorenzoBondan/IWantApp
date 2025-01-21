using IWantApp.Models.Base;
using IWantApp.Utils;
using System.Linq.Expressions;

namespace IWantApp.Repositories.Base;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties);
    Task<List<T>> FindWithPagedSearch(string query);
    Task<PageableResponse<T>> GetAllPaged(int page, int size, string? sortedBy = null, Expression<Func<T, bool>>? filter = null, params Expression<Func<T, object>>[] includeProperties);
    Task<T?> FindById(int id, Expression<Func<T, object>> includeProperty);
    Task<T> Create(T obj);
    Task<T?> Update(T obj);
    Task Delete(int id);
    Task<bool> ExistsById(int id);
    Task<int> GetCount(Expression<Func<T, bool>> predicate);
}
