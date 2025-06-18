
using Waaa.Application.Dto;
using Waaa.Domain.Models;

namespace Waaa.Application.Interfaces
{
    public interface IWebinarService
    {
        Task<int> RegisterWebinarAsync(WebinarDto user);
        Task<bool> AddPaymentAsync(int id);
        Task<IEnumerable<WebinarRegistrations>> GetWebinarRegistrationsAsync();
    }
}
