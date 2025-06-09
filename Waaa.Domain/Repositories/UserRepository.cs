using Waaa.Domain.Entities;
using Waaa.Domain.Interfaces;

namespace Waaa.Domain.Repositories
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        public async Task<int> AddUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            var res = await dbContext.SaveChangesAsync();
            return user.Id;
        }

        public IEnumerable<User> GetUsers()
        {
            return dbContext.Users;
        }
    }
}
