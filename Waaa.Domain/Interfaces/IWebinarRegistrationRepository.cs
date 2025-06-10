using Waaa.Domain.Entities;

namespace Waaa.Domain.Interfaces
{
    public interface IWebinarRegistrationRepository
    {
        Task<int> AddRegistrationAsync(WebinarRegistration registration);
        Task<WebinarRegistration?> GetRegistrationByIdAsync(int id);
        Task<WebinarRegistration> GetRegistrationsByUserIdAsync(int userId);
        Task<IEnumerable<WebinarRegistration>> GetRegistrationsByWebinarIdAsync(int webinarId);
        Task<bool> UpdateRegistrationAsync(WebinarRegistration registration);
    }
}
