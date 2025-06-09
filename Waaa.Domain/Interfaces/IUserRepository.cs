using Waaa.Domain.Entities;

namespace Waaa.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(User user);
        IEnumerable<User> GetUsers();
    }
}
