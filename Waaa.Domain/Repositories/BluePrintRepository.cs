using Microsoft.EntityFrameworkCore;
using Waaa.Domain.Entities;
using Waaa.Domain.Interfaces;

namespace Waaa.Domain.Repositories
{
    public class BluePrintRepository(AppDbContext dbContext) : IBluePrintRepository
    {
        public async Task<int> AddBluePrintAsync(BluePrint bluePrint)
        {
            await dbContext.BluePrints.AddAsync(bluePrint);
            var res = await dbContext.SaveChangesAsync();
            return res > 0 ? res : 0;
        }

        public async Task<BluePrint> GetRegistrationsByUserIdAsync(int userId)
        {
            return await dbContext.BluePrints.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }
    }
}
