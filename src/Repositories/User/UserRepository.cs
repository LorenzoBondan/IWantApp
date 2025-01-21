using IWantApp.Models;
using IWantApp.Models.Context;
using IWantApp.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAll()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<PageableResponse<User>> GetAllPaged(int page, int size, string? sortedBy = null)
    {
        var totalCount = await _context.Users.CountAsync();

        IQueryable<User> query = _context.Users;

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

        return new PageableResponse<User>
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

    public async Task<User> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public User GetByUsername(string username)
    {
        var user = _context.Users
        .Where(u => u.UserName == username)
        .FirstOrDefault();

        if (user != null)
        {
            var roles = _context.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Select(ur => ur.RoleId)
                .ToList();

            user.UserRoles = roles.Select(roleId => new IdentityUserRole<int> { RoleId = roleId }).ToList();
        }

        return user;
    }

    public async Task Create(User obj)
    {
        await _context.Users.AddAsync(obj);
        await _context.SaveChangesAsync();
    }

    public async Task Update(User obj)
    {
        _context.Users.Update(obj);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var obj = await GetById(id);
        if (obj != null)
        {
            _context.Users.Remove(obj);
            await _context.SaveChangesAsync();
        }
    }
}
