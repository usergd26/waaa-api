using Waaa.Domain.Entities;
using Waaa.Domain.Interfaces;

namespace Waaa.Domain.Repositories
{
    public class UserRepository(AppDbContext dbContext) : IUserRepository
    {
        public int AddUser(User user)
        {
            dbContext.Users.Add(user);
            var res = dbContext.SaveChanges();
            return res;
        }
    }
}
