using IWantApp.Models;
using IWantApp.Models.Context;
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
