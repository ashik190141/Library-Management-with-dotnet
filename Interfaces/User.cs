using Library_Management.Models;
using Library_Management.Helper;

namespace Library_Management.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(User user);
        Task<IEnumerable<User>> GetAllUserAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> UpdateUserAsync(int id, UpdateUserDto user, User existingUser);
        Task<int> DeleteUserAsync(int id);
    }

    public interface IUserService
    {
        Task<StandardApiResponse<User>> AddUserAsync(User user);
        Task<StandardApiResponse<IEnumerable<User>>> GetAllUserAsync();
        Task<StandardApiResponse<User>> GetUserByIdAsync(int id);
        Task<StandardApiResponse<User>> UpdateUserAsync(int id, UpdateUserDto book);
        Task<StandardApiResponse<User>> DeleteUserAsync(int id);
    }
}
