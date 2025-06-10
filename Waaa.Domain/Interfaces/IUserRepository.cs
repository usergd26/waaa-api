using Waaa.Domain.Entities;

namespace Waaa.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(User user);
        IEnumerable<User> GetUsers();
        Task<User> GetUserByEmailOrPhoneAsync(string email, string phone);
    }
}
