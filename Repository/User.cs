using Library_Management.Data;
using Library_Management.Interfaces;
using Library_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Library_Management.Repositories;

public class UserRepository(LibraryContext context) : BaseRepository(context), IUserRepository
{
    public async Task<int> AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        int response = await SaveChangesAsync();
        if (response > 0)
        {
            return user.Id;
        }
        return 0;
    }

    public async Task<IEnumerable<User>> GetAllUserAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> UpdateUserAsync(int id, UpdateUserDto user, User existingUser)
    {
        existingUser.Name = user.Name ?? existingUser.Name;
        existingUser.Email = user.Email ?? existingUser.Email;
        existingUser.UpdatedBy = 1;
        existingUser.UpdatedAt = DateTime.SpecifyKind(DateTime.UtcNow, DateTimeKind.Utc);
        existingUser.CreatedAt = DateTime.SpecifyKind(existingUser.CreatedAt, DateTimeKind.Utc);

        _context.Users.Update(existingUser);
        await SaveChangesAsync();

        return existingUser;
    }

    public async Task<int> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if(user != null)
        {
            _context.Users.Remove(user);
            return await SaveChangesAsync();
        }
        return 0;
    }
}