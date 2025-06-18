using Microsoft.Extensions.Logging;
using Waaa.Application.Interfaces;
using Waaa.Domain.Interfaces;
using Waaa.Application.Models;

namespace Waaa.Application.Services
{
    public class UserService(IUserRepository _userRepository, ILogger<UserService> _logger) : IUserService
    {
        public async Task<int> AddUserAsync(UserDto user)
        {
            var userId = await _userRepository.AddUserAsync(new Domain.Entities.AppUser { Name = user.Name, Email = user.Email, Phone = user.Phone });
            if (userId > 0)
                _logger.LogInformation("User added successfully: {UserId}", userId);

            return userId;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return users.Select(u => new UserDto { Id = u.Id, Name = u.Name, Email = u.Email, Phone = u.Phone });
        }
    }
}
