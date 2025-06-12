using Waaa.Application.Interfaces;
using Waaa.Domain.Interfaces;
using Waaa.Application.Models;

namespace Waaa.Application.Services
{
    public class WebinarRegistrationService(IUserRepository userRepository, IWebinarRegistrationRepository webinarRegistrationRepository) : IWebinarRegistrationService
    {
        public async Task<int> RegisterWebinarAsync(User user)
        {
            var userId = userRepository.GetUserByEmailOrPhoneAsync(user.Email, user.Phone).Result?.Id ?? 0;

            if (userId != 0) 
            {
                var existingRegistrations = await webinarRegistrationRepository.GetRegistrationsByUserIdAsync(userId);

                if (existingRegistrations != null)
                {
                    return 0;
                }
            }

            if (userId == 0)
            {
                userId = await userRepository.AddUserAsync(new Domain.Entities.User
                {
                    Name = user.Name,
                    Email = user.Email,
                    Phone = user.Phone
                });
            }


           var regId =  await webinarRegistrationRepository.AddRegistrationAsync(new Domain.Entities.WebinarRegistration
            {
                UserId = userId,
                WebinarId = 1,
                PaymentStatus = false,
            });
            return regId;
        }
    }
}
