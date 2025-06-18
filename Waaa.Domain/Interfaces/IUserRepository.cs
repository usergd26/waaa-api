using Waaa.Domain.Entities;

namespace Waaa.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(AppUser user);
        IEnumerable<AppUser> GetUsers();
        Task<AppUser> GetUserByEmailOrPhoneAsync(string email, string phone);
    }
}
