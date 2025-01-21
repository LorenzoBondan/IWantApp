using IWantApp.Models.Base;
using IWantApp.Models.Context;
using IWantApp.Services.Exceptions;
using IWantApp.Utils;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace IWantApp.Repositories.Base;

public class GenericRepository<T> : IRepository<T> where T : BaseEntity
{
    protected ApplicationDbContext _context;
    private DbSet<T> _dbSet;

    public GenericRepository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        // Para cada propriedade incluída, aplica o Include
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return query;
    }

    public async Task<List<T>> FindWithPagedSearch(string query)
    {
        return await _dbSet.FromSqlRaw(query).ToListAsync();
    }

    public async Task<PageableResponse<T>> GetAllPaged(
    int page,
    int size,
    string? sortedBy = null,
    Expression<Func<T, bool>>? filter = null,
    params Expression<Func<T, object>>[] includeProperties)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        // Aplicar filtro, se houver
        if (filter != null)
        {
            query = query.Where(filter);
        }

        // Aplicar Includes
        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        // Ordenação dinâmica
        if (!string.IsNullOrEmpty(sortedBy))
        {
            var sortParts = sortedBy.Split(';');
            var sortColumn = sortParts[0];
            var ascending = sortParts.Length < 2 || sortParts[1].ToLower() == "a";

            query = ascending ? query.OrderByDynamic(sortColumn) : query.OrderByDescendingDynamic(sortColumn);
        }

        // Total de itens
        var totalCount = await query.CountAsync();

        // Paginação
        var content = await query
            .Skip(page * size)
            .Take(size)
            .ToListAsync();

        // Total de páginas
        var totalPages = (int)Math.Ceiling((double)totalCount / size);

        return new PageableResponse<T>
        {
            NumberOfElements = content.Count,
            Page = page,
            TotalPages = totalPages,
            Size = size,
            First = page == 0,
            Last = page >= totalPages - 1,
            SortedBy = sortedBy,
            Content = content
        };
    }

    public async Task<T?> FindById(int id, Expression<Func<T, object>> includeProperty = null)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();

        if (includeProperty != null)
        {
            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<T> Create(T obj)
    {
        await _dbSet.AddAsync(obj);
        await SaveChangesAsync();
        return obj;
    }

    public async Task<T?> Update(T obj)
    {
        var existingItem = await _dbSet.FirstOrDefaultAsync(p => p.Id == obj.Id);
        if (existingItem == null) return null;

        _context.Entry(existingItem).CurrentValues.SetValues(obj);
        await SaveChangesAsync();
        return existingItem;
    }

    public async Task Delete(int id)
    {
        var item = await _dbSet.FirstOrDefaultAsync(p => p.Id == id);
        if (item != null)
        {
            try
            {
                _dbSet.Remove(item);
                await SaveChangesAsync();

            } catch (DbUpdateException e)
            {
                throw new DatabaseException("Existem dependências vinculadas a este objeto.");
            }
        }
    }

    public async Task<bool> ExistsById(int id)
    {
        return await _dbSet.AnyAsync(p => p.Id == id);
    }

    public async Task<int> GetCount(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.CountAsync(predicate);
    }

    private async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
