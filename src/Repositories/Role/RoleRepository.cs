using IWantApp.Models;
using IWantApp.Models.Context;
using IWantApp.Utils;
using Microsoft.EntityFrameworkCore;

public class RoleRepository : IRoleRepository
{
    private readonly ApplicationDbContext _context;

    public RoleRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Role>> GetAll()
    {
        return await _context.Roles.ToListAsync();
    }

    public async Task<PageableResponse<Role>> GetAllPaged(int page, int size, string? sortedBy = null)
    {
        var totalCount = await _context.Roles.CountAsync();

        IQueryable<Role> query = _context.Roles;

        // Ordenação
        if (!string.IsNullOrEmpty(sortedBy))
        {
            var sortParts = sortedBy.Split(';');
            var sortColumn = sortParts[0];
            var ascending = sortParts.Length < 2 || sortParts[1].ToLower() == "a";

            query = ascending ? query.OrderByDynamic(sortColumn) : query.OrderByDescendingDynamic(sortColumn);
        }

        // Paginação
        var content = await query
            .Skip(page * size)
            .Take(size)
            .ToListAsync();

        // Total de páginas
        var totalPages = (int)Math.Ceiling((double)totalCount / size);

        return new PageableResponse<Role>
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

    public async Task<Role> GetById(int id)
    {
        return await _context.Roles.FindAsync(id);
    }

    public async Task Create(Role obj)
    {
        await _context.Roles.AddAsync(obj);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Role obj)
    {
        _context.Roles.Update(obj);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var obj = await GetById(id);
        if (obj != null)
        {
            _context.Roles.Remove(obj);
            await _context.SaveChangesAsync();
        }
    }
}
