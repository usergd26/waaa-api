using Microsoft.EntityFrameworkCore;
using Waaa.Domain.Entities;
using Waaa.Domain.Interfaces;

namespace Waaa.Domain.Repositories
{
    public class WebinarRegistrationRepository(AppDbContext dbContext ) : IWebinarRegistrationRepository
    {
        async Task<int> IWebinarRegistrationRepository.AddRegistrationAsync(WebinarRegistration registration)
        {
            await dbContext.WebinarRegistrations.AddAsync(registration);
            await dbContext.SaveChangesAsync();
            return registration.Id;
        }

        Task<WebinarRegistration?> IWebinarRegistrationRepository.GetRegistrationByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        async Task<WebinarRegistration> IWebinarRegistrationRepository.GetRegistrationsByUserIdAsync(int userId)
        {
            return await dbContext.WebinarRegistrations.FirstOrDefaultAsync(x => x.UserId == userId);
        }

        Task<IEnumerable<WebinarRegistration>> IWebinarRegistrationRepository.GetRegistrationsByWebinarIdAsync(int webinarId)
        {
            throw new NotImplementedException();
        }

        Task<bool> IWebinarRegistrationRepository.UpdateRegistrationAsync(WebinarRegistration registration)
        {
            throw new NotImplementedException();
        }
    }
}
