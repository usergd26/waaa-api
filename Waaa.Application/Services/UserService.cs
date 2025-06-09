using Microsoft.Extensions.Logging;
using Waaa.Application.Interfaces;
using Waaa.Domain.Interfaces;
using Waaa.Infrastructure.Models;

namespace Waaa.Application.Services
{
    public class UserService(IUserRepository _userRepository, ILogger<UserService> _logger) : IUserService
    {
        public async Task<int> AddUserAsync(User user)
        {
            var userId = await _userRepository.AddUserAsync(new Domain.Entities.User { Name = user.Name, Email = user.Email, Phone = user.Phone });
            if (userId > 0)
                _logger.LogInformation("User added successfully: {UserId}", userId);

            return userId;
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return users.Select(u => new User { Id = u.Id, Name = u.Name, Email = u.Email, Phone = u.Phone });
        }
    }
}
