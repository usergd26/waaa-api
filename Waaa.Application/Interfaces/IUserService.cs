using Waaa.Application.Dto;

namespace Waaa.Application.Interfaces
{
    public interface IUserService
    {
        Task<int> AddUserAsync(UserDto user);
        Task<IEnumerable<UserDto>> GetUsersAsync();
    }
}
