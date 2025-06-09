using Waaa.Infrastructure.Models;

namespace Waaa.Application.Interfaces
{
    public interface IUserService
    {
        Task<int> AddUserAsync(User user);
        IEnumerable<User> GetUsers();
    }
}
