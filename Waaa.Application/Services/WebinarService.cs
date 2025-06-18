using Waaa.Application.Interfaces;
using Waaa.Domain.Interfaces;
using Waaa.Application.Dto;
using Waaa.Domain.Models;

namespace Waaa.Application.Services
{
    public class WebinarService(IUserRepository userRepository, IWebinarRepository webinarRegistrationRepository) : IWebinarService
    {
        public async Task<int> RegisterWebinarAsync(WebinarDto webinarDto)
        {
            var userId = userRepository.GetUserByEmailOrPhoneAsync(webinarDto.Email, webinarDto.Phone).Result?.Id ?? 0;

            if (userId != 0)
            {
                var existingRegistrations = await webinarRegistrationRepository.GetRegistrationsByUserIdAsync(userId);

                if (existingRegistrations != null)
                {
                    return 0;
                }
            }

            else
            {
                userId = await userRepository.AddUserAsync(new Domain.Entities.AppUser
                {
                    Name = webinarDto.Name,
                    Email = webinarDto.Email,
                    Phone = webinarDto.Phone
                });
            }

            var regId = await webinarRegistrationRepository.AddRegistrationAsync(new Domain.Entities.WebinarRegistration
            {
                UserId = userId,
                WebinarId = webinarDto.WebinarId,
                PaymentStatus = false,
            });
            return regId;
        }

        public async Task<bool> AddPaymentAsync(int id)
        {
            return await webinarRegistrationRepository.AddPaymentAsync(id);
        }

        public async Task<IEnumerable<WebinarRegistrations>> GetWebinarRegistrationsAsync()
        {
            return await webinarRegistrationRepository.GetRegistrationsAsync();
        }
    }
}
