using Microsoft.Extensions.Logging;
using Waaa.Application.Interfaces;
using Waaa.Domain.Interfaces;
using Waaa.Application.Models;

namespace Waaa.Application.Services
{
    public class UserService(IUserRepository userRepository, ILogger<UserService> logger) : IUserService
    {
        public async Task<int> AddUserAsync(UserDto user)
        {
            var userId = await userRepository.AddUserAsync(new Domain.Entities.AppUser { Name = user.Name, Email = user.Email, Phone = user.Phone });
            if (userId > 0)
                logger.LogInformation("User added successfully: {UserId}", userId);

            return userId;
        }

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await userRepository.GetUsersAsync();
            return users.Select(u => new UserDto { Id = u.Id, Name = u.Name, Email = u.Email, Phone = u.Phone });
        }
    }
}
