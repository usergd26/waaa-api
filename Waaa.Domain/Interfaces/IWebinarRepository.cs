using Waaa.Domain.Entities;
using Waaa.Domain.Models;

namespace Waaa.Domain.Interfaces
{
    public interface IWebinarRepository
    {
        Task<int> AddRegistrationAsync(WebinarRegistration registration);
        Task<WebinarRegistration?> GetRegistrationByIdAsync(int id);
        Task <IEnumerable<WebinarRegistrations>> GetRegistrationsAsync();
        Task<WebinarRegistration> GetRegistrationsByUserIdAsync(int userId);
        Task<IEnumerable<WebinarRegistration>> GetRegistrationsByWebinarIdAsync(int webinarId);
        Task<bool> UpdateRegistrationAsync(WebinarRegistration registration);
        Task<bool> AddPaymentAsync(int id);
    }
}
