using Waaa.Application.Interfaces;
using Waaa.Domain.Interfaces;
using Waaa.Infrastructure.Models;

namespace Waaa.Application.Services
{
    public class UserService(IUserRepository _userRepository) : IUserService
    {
        public int AddUser(User user)
        {
            return _userRepository.AddUser(new Domain.Entities.User { Name = user.Name, Email = user.Email, Phone = user.Phone });
        }

        public IEnumerable<User> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return users.Select(u => new User { Id = u.Id, Name = u.Name, Email = u.Email, Phone = u.Phone });
        }
    }
}
