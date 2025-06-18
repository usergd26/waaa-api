using Waaa.Domain.Entities;

namespace Waaa.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(AppUser user);
        Task<IEnumerable<AppUser>> GetUsersAsync();
        Task<AppUser> GetUserByEmailOrPhoneAsync(string email, string phone);
    }
}
