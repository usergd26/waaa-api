
using Waaa.Application.Models;

namespace Waaa.Application.Interfaces
{
    public interface IWebinarService
    {
        Task<int> RegisterWebinarAsync(WebinarDto user);
        Task<bool> AddPaymentAsync(int id);
    }
}
