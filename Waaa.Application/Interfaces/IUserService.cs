using Waaa.Application.Models;

namespace Waaa.Application.Interfaces
{
    public interface IUserService
    {
        Task<int> AddUserAsync(UserDto user);
        IEnumerable<UserDto> GetUsers();
    }
}
