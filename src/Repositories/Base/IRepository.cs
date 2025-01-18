using IWantApp.Models.Base;
using System.Linq.Expressions;

namespace IWantApp.Repositories.Base;

public interface IRepository<T> where T : BaseEntity
{
    IQueryable<T> FindAll();
    Task<List<T>> FindWithPagedSearch(string query);
    Task<T?> FindById(int id);
    Task<T> Create(T item);
    Task<T?> Update(T item);
    Task Delete(int id);
    Task<bool> ExistsById(int id);
    Task<int> GetCount(Expression<Func<T, bool>> predicate);
}
