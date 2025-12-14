using Library_Management.Interfaces;
using Library_Management.Models;
using Library_Management.Helper;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Library_Management.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<StandardApiResponse<User>> AddUserAsync(User user)
        {
            user.CreatedBy = 0;
            user.UpdatedBy = 0;
            user.Password = await HashedPassword(user.Password);
            int res = await _userRepository.AddUserAsync(user);
            if (res > 0)
            {
                return new StandardApiResponse<User>(true, "User added successfully", user);
            }
            else
            {
                return new StandardApiResponse<User>(false, "Failed to add user", null);
            }
        }

        public async Task<StandardApiResponse<IEnumerable<User>>> GetAllUserAsync()
        {
            var users = await _userRepository.GetAllUserAsync();
            return new StandardApiResponse<IEnumerable<User>>(true, "Users retrieved successfully", users);
        }

        public async Task<StandardApiResponse<User>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null){
                return new StandardApiResponse<User>(false, "User not found", null);
            }
            return new StandardApiResponse<User>(true, "Users retrieved successfully", user);
        }

        public async Task<StandardApiResponse<User>> UpdateUserAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if(user == null){
                return new StandardApiResponse<User>(true, "User Update successfully", null);
            }
            var updatedUser = await _userRepository.UpdateUserAsync(id, updateUserDto, user);
            return new StandardApiResponse<User>(true, "User Update successfully", updatedUser);
        }

        public async Task<StandardApiResponse<User>> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            var removedUser = await _userRepository.DeleteUserAsync(id);
            if(removedUser == 0)
            {
                return new StandardApiResponse<User>(false, "Failed to delete user", null);
            }
            return new StandardApiResponse<User>(true, "User Delete successfully", user);
        }

        private Task<string> HashedPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(128 / 8);
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: password!,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

            return Task.FromResult(hashed);
        }
    }
}