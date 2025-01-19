using IWantApp.Models;
using IWantApp.Models.Context;
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

    public async Task<User> GetById(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public User GetByUsername(string username)
    {
        return _context.Users.Where(u => u.UserName == username).FirstOrDefault();
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
