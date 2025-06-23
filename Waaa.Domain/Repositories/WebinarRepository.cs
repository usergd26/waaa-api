using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Waaa.Domain.Entities;
using Waaa.Domain.Interfaces;
using Waaa.Domain.Models;

namespace Waaa.Domain.Repositories
{
    public class WebinarRepository(AppDbContext dbContext) : IWebinarRepository
    {
        public async Task<int> AddRegistrationAsync(WebinarRegistration registration)
        {
            await dbContext.WebinarRegistrations.AddAsync(registration);
            await dbContext.SaveChangesAsync();
            return registration.Id;
        }

        public Task<WebinarRegistration?> GetRegistrationByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<WebinarRegistration> GetRegistrationsByUserIdAsync(int userId)
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

        public Task<IEnumerable<WebinarRegistration>> GetRegistrationsByWebinarIdAsync(int webinarId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateRegistrationAsync(WebinarRegistration registration)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<WebinarRegistrations>> GetRegistrationsAsync()
        {
            return await (from reg in dbContext.WebinarRegistrations.AsNoTracking()
                      join user in dbContext.AppUsers.AsNoTracking()
                      on reg.UserId equals user.Id
                      join web in dbContext.Webinars.AsNoTracking()
                      on reg.WebinarId equals web.Id
                      select new WebinarRegistrations
                      {
                          RegistrationId = reg.Id,
                          Name = user.Name,
                          Email = user.Email,
                          Phone = user.Phone,
                          WebinarId = web.Id,
                          WebinarName = web.Name,
                          PaymentStatus = reg.PaymentStatus
                      }).ToListAsync();
        }
    }
}
