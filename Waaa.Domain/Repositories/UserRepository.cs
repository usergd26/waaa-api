using Microsoft.EntityFrameworkCore;
using Waaa.Domain.Entities;
using Waaa.Domain.Interfaces;

namespace Waaa.Domain.Repositories
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        public async Task<int> AddUserAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user.Id;
        }

        public async Task<User> GetUserByEmailOrPhoneAsync(string email, string phone)
        {
            return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email || u.Phone == phone);
        }

        public IEnumerable<User> GetUsers()
        {
            return dbContext.Users;
        }
    }
}
