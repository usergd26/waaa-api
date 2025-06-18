using Waaa.Application.Interfaces;
using Waaa.Application.Models;
using Waaa.Domain.Interfaces;
using Waaa.Domain.Repositories;

namespace Waaa.Application.Services
{
    public class BluePrintService (IBluePrintRepository bluePrintRepository, IUserRepository userRepository) : IBluePrintService
    {
        public async Task<int> RegisterForBluePrintAsync(BluePrintDto bluePrintDto)
        {
            var userId = userRepository.GetUserByEmailOrPhoneAsync(bluePrintDto.Email, bluePrintDto.Phone).Result?.Id ?? 0;

            if (userId != 0)
            {
                var existingRegistrations = await bluePrintRepository.GetRegistrationsByUserIdAsync(userId);

                if (existingRegistrations != null)
                {
                    return 0;
                }
            }

            else
            {
                userId = await userRepository.AddUserAsync(new Domain.Entities.AppUser
                {
                    Name = bluePrintDto.Name,
                    Email = bluePrintDto.Email,
                    Phone = bluePrintDto.Phone
                });
            }

            return await bluePrintRepository.AddBluePrintAsync(new Domain.Entities.BluePrint {UserId = userId });
        }
    }
}
