using Microsoft.EntityFrameworkCore;
using Waaa.Domain.Entities;
using Waaa.Domain.Interfaces;

namespace Waaa.Domain.Repositories
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        public async Task<int> AddUserAsync(AppUser user)
        {
            await dbContext.AppUsers.AddAsync(user);
            var res = await dbContext.SaveChangesAsync();
            return user.Id;
        }

        public IEnumerable<AppUser> GetUsers()
        {
            return dbContext.AppUsers;
        }
        public async Task<AppUser> GetUserByEmailOrPhoneAsync(string email, string phone)
        {
            return await dbContext.AppUsers.FirstOrDefaultAsync(u => u.Email == email || u.Phone == phone);
        }
    }
}
