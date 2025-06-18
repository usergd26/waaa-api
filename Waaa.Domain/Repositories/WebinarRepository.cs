using Microsoft.EntityFrameworkCore;
using Waaa.Domain.Entities;
using Waaa.Domain.Interfaces;

namespace Waaa.Domain.Repositories
{
    public class WebinarRepository(AppDbContext dbContext ) : IWebinarRepository
    {  
        async Task<int> IWebinarRepository.AddRegistrationAsync(WebinarRegistration registration)
        {
            await dbContext.WebinarRegistrations.AddAsync(registration);
            await dbContext.SaveChangesAsync();
            return registration.Id;
        }

        Task<WebinarRegistration?> IWebinarRepository.GetRegistrationByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<WebinarRegistration> IWebinarRepository.GetRegistrationsByUserIdAsync(int userId)
        {
            return await dbContext.WebinarRegistrations.AsNoTracking()
                .FirstOrDefaultAsync(x => x.UserId == userId);
        }

        public async Task<bool> AddPaymentAsync(int id)
        {
            var result = await dbContext.WebinarRegistrations.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null) return false;

            result.PaymentStatus = true;
            return await dbContext.SaveChangesAsync() > 0;
        }

        Task<IEnumerable<WebinarRegistration>> IWebinarRepository.GetRegistrationsByWebinarIdAsync(int webinarId)
        {
            throw new NotImplementedException();
        }

        Task<bool> IWebinarRepository.UpdateRegistrationAsync(WebinarRegistration registration)
        {
            throw new NotImplementedException();
        }
    }
}
