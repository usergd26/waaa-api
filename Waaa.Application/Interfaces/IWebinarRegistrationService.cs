
using Waaa.Application.Models;

namespace Waaa.Application.Interfaces
{
    public interface IWebinarRegistrationService
    {
        Task<int> RegisterWebinarAsync(User user);
        Task<bool> AddPaymentAsync(int id);
    }
}
