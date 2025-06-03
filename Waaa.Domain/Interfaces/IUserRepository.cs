using Waaa.Domain.Entities;

namespace Waaa.Domain.Interfaces
{
    public interface IUserRepository
    {
        int AddUser(User user);
    }
}
